using System;
using System.Threading.Tasks;
using MarksAssets.VideoPlayerWebGL;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleVideoPlayer : MonoBehaviour, IDragHandler, IPointerDownHandler {
    [SerializeField]
    private VideoPlayerWebGL videoPlayer;

    [SerializeField]
    private AspectRatioFitter aspectRatioFitter;

    [SerializeField]
    private Text currentTime;

    [SerializeField]
    private Text totalTime;

    [SerializeField]
    private Image progress;

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private Slider playbackSlider;

    [SerializeField]
    private Image background;

    private double videoLength;

    private void Start() {
        volumeSlider.onValueChanged.AddListener(v => {
            videoPlayer.Volume(v);
            if (v == 0.0f) videoPlayer.Muted(true);//for iOS Safari. Volume control doesn't work, only mute does https://developer.apple.com/library/archive/documentation/AudioVideo/Conceptual/Using_HTML5_Audio_Video/Device-SpecificConsiderations/Device-SpecificConsiderations.html
            else videoPlayer.Muted(false);
        });
        playbackSlider.onValueChanged.AddListener(v => videoPlayer.PlaybackSpeed(v));
    }

    private void Update() {
        var currenTime = videoPlayer.Time();
        var buffered = videoPlayer.buffered;

        progress.fillAmount = (float)(currenTime / videoLength);
        currentTime.text = TimeSpan.FromSeconds(currenTime).ToString("mm\\:ss");

        for (uint i = 0; i < buffered.length; ++i) {//see https://developer.mozilla.org/en-US/docs/Web/Guide/Audio_and_video_delivery/buffering_seeking_time_ranges#creating_our_own_buffering_feedback
            if (buffered.end(buffered.length - 1 - i) > currenTime)//this is to give similar behavior on Android and Apple devices, instead of checking if start < videoPlayer.Time()
                background.fillAmount = (float)buffered.end(buffered.length - 1 - i) / (float)videoLength;
        }
        
    }

    public void OnDrag(PointerEventData eventData) {
        TrySkip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData) {
        TrySkip(eventData);
    }

    private void TrySkip(PointerEventData eventData) {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint)) {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }

    private async void SkipToPercent(float pct) {
        bool isPlaying = videoPlayer.IsPlaying();
        videoPlayer.Time(videoPlayer.length * pct);
        await DelayAsync(0);
        if (isPlaying) videoPlayer.Play();
    }

    public static async Task DelayAsync(float secondsDelay) {
        float startTime = Time.time;
        while (Time.time <= startTime + secondsDelay) await Task.Yield();
    }

    public void LoadedData() {
        aspectRatioFitter.aspectRatio = (float)videoPlayer.Width() / (float)videoPlayer.Height();
        totalTime.text = TimeSpan.FromSeconds(videoPlayer.length).ToString("mm\\:ss");
        videoLength = videoPlayer.length;
    }

    public void PlayPause() {
        if (videoPlayer.IsPaused()) {
            videoPlayer.PlayPointerDown();
        } else {
            videoPlayer.Pause();
        }
    }

    public void SetVolumePlayback() {
        videoPlayer.Muted(volumeSlider.value == 0.0f ? true : false);
        videoPlayer.Volume(volumeSlider.value);
        videoPlayer.PlaybackSpeed(playbackSlider.value);
    }
}