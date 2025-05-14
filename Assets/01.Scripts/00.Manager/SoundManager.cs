using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> bgmDict = new();
    private Dictionary<string, AudioClip> sfxDict = new();


    private void InitDictionaries()
    {
        foreach (var clip in bgmClips)
        {
            if (clip != null && !bgmDict.ContainsKey(clip.name))
                bgmDict.Add(clip.name, clip);
        }

        foreach (var clip in sfxClips)
        {
            if (clip != null && !sfxDict.ContainsKey(clip.name))
                sfxDict.Add(clip.name, clip);
        }
    }

    // BGM 재생
    public void PlayBGM(string clipName, bool loop = true)
    {
        if (bgmDict.TryGetValue(clipName, out var clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{clipName}' not found.");
        }
    }

    // SFX 재생
    public void PlaySFX(string clipName)
    {
        if (sfxDict.TryGetValue(clipName, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{clipName}' not found.");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
