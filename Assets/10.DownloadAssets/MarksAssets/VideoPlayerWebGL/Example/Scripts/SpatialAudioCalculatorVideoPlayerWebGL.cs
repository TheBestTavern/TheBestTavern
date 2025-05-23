using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarksAssets.VideoPlayerWebGL;

public class SpatialAudioCalculatorVideoPlayerWebGL : MonoBehaviour
{
    [Tooltip("Volume of audio source when you are close to it (closer than 'Distance Begin Falloff Curve').")]
    [Range(0.0f, 1.0f)] public float maxVolume = 1.0f;
    [Tooltip("Distance where the smooth falloff curve begins. If distance is less than this, we are at full volume.")]
    public float distanceBeginFalloffCurve = 3.0f;
    [Tooltip("Distance where smooth falloff curve ends. If distance is greater than this, we are at zero volume.")]
    public float distanceEndFalloffCurve = 10.0f;

    [Header("References")]
    [Tooltip("The position of this transform is used to calculate spatial audio in relation to the active Audio Listener in the scene. If you want audio to come from the video screen, place this transform at the very center of the video screen.")]
    public Transform audioSourceTransform;
    
    [Header("Debug")]
    [SerializeField] [Range(0.0f, 1.0f)] private float currentVolume = 0.0f;
    [SerializeField] [Range(-1.0f, 1.0f)] private float currentPan = 0.0f;

    // Private variables
    private AudioListener audioListener;
    private VideoPlayerWebGL videoPlayerWebGL;

    private void Start()
    {
        if (videoPlayerWebGL == null)
            videoPlayerWebGL = GetComponent<VideoPlayerWebGL>();

        if (videoPlayerWebGL == null)
        {
            if (Application.isEditor)
                Debug.LogError("'SpatialAudioCalculatorVideoPlayerWebGL' script must be attached to the same game object as the VideoPlayerWebGL script.");
        }
        else
        {
            videoPlayerWebGL.ForceMono(true);   // Ensure that we are panning a mono audio source (required for correct 3D audio spatialization).
        }
    }

    private void Update()
    {
        if (videoPlayerWebGL != null && videoPlayerWebGL.enabled)
            UpdateSpatialAudio();
    }

    private void UpdateSpatialAudio()
    {
        if (audioListener == null || !audioListener.enabled || !audioListener.gameObject.activeInHierarchy)
            audioListener = GetActiveAudioListener(); // Get the active audio listener in the scene (if neccessary).

        if (audioListener == null)  // If there is no active audio listener in the scene, set the volume to zero (no need to adjust pan).
        {
            currentVolume = 0.0f;
        }
        else
        {
            currentVolume = GetProximityVolume();

            if (currentVolume > 0.0f)   // Only calculate pan if we can actually hear the audio source.
                currentPan = GetPan();
        }        

        videoPlayerWebGL.Volume(currentVolume);
        videoPlayerWebGL.Pan(currentPan);
    }

    private float GetPan()  // Source (public article from Agora): https://www.agora.io/en/blog/implementing-spatial-audio-chat-in-unity-using-agora/
    {
        Vector3 directionToAudioSource = audioSourceTransform.position - audioListener.transform.position;
        directionToAudioSource.Normalize();
        float pan = Vector3.Dot(audioListener.transform.right, directionToAudioSource);
        return pan;
    }

    private float GetProximityVolume()
    {
        float distance = Vector3.Distance(audioListener.transform.position, audioSourceTransform.position);
        float calculatedVolume;

        if (distance >= distanceEndFalloffCurve) // Zero volume if audio listener is further than end falloff distance.
            calculatedVolume = 0.0f;
        else if (distance <= distanceBeginFalloffCurve)  // Max volume if audio listener is closer than the begin falloff distance.
            calculatedVolume = 1.0f * maxVolume;
        else  // Otherwise, calculate the falloff curve using the Mathf.SmoothStep function for a smooth transition.
        {
            float falloffCurveVolume = Mathf.SmoothStep(1.0f, 0.0f, (distance - distanceBeginFalloffCurve) / (distanceEndFalloffCurve - distanceBeginFalloffCurve));  // A smooth 1 to 0 as user gets further away.
            calculatedVolume = falloffCurveVolume * maxVolume;
        }

        return calculatedVolume;
    }

    private AudioListener GetActiveAudioListener()
    {
        AudioListener[] audioListenersInScene = GameObject.FindObjectsOfType<AudioListener>();

        for (int i = 0; i < audioListenersInScene.Length; i++)
        {
            if (audioListenersInScene[i].enabled && audioListenersInScene[i].gameObject.activeInHierarchy)
                return audioListenersInScene[i];   // Find an enabled audio listener.
        }

        return null;    // Return null if no active audio lister was found.
    }
}
