using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;


    [Header("Audio Clips")]
    [SerializeField] private SoundLibrary soundLibrary;
    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
    }

    protected override void Awake()
    {

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        AddBGM();
        AddSFX();
        base.Awake();
    }

    private void Start()
    {
        PlayBGM("MainBGM");
    }


    public void PlayBGM(string name)
    {
        var data = System.Array.Find(soundLibrary.bgmClips, x => x.soundName == name);
        if (data != null)
        {
            bgmSource.clip = data.clip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        var data = System.Array.Find(soundLibrary.sfxClips, x => x.soundName == name);
        if (data != null)
        {
            sfxSource.PlayOneShot(data.clip);
        }
    }

    private void AddBGM()
    {
        foreach (var bgm in soundLibrary.bgmClips)
        {
            if (!bgmDict.ContainsKey(bgm.soundName))
                bgmDict.Add(bgm.soundName, bgm.clip);
        }
    }

    private void AddSFX()
    {
        foreach (var sfx in soundLibrary.sfxClips)
        {
            if (!sfxDict.ContainsKey(sfx.soundName))
                sfxDict.Add(sfx.soundName, sfx.clip);
        }
    }

    public void SetBGMVolume(float volume) => bgmSource.volume = volume;
    public void SetSFXVolume(float volume) => sfxSource.volume = volume;

    public float GetBGMVolume() => bgmSource.volume;
    public float GetSFXVolume() => sfxSource.volume;
}
