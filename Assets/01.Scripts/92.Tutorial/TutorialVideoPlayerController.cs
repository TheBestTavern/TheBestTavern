using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialVideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoRawImage;

    public VideoClip[] clips;


    public void PlayTutorialVideo(int index)
    {
        videoRawImage.gameObject.SetActive(false);
        videoPlayer.Stop();

        videoPlayer.clip = clips[index];
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        videoPlayer.prepareCompleted -= OnPrepared; // 중복 등록 방지

        videoRawImage.gameObject.SetActive(true); // 이제 표시
        videoPlayer.Play();
    }

    public void StopTutorialVideo()
    {
        videoPlayer.Stop();
        videoRawImage.gameObject.SetActive(false);
    }
}
