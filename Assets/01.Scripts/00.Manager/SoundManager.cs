using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : MonoSingleton<SoundManager>
{

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopSource;



    [Header("Audio Clips")]
    [SerializeField] private SoundLibrary soundLibrary;
    private Dictionary<string, string> bgmKeys = new Dictionary<string, string>();
    private Dictionary<string, string> sfxKeys = new Dictionary<string, string>();
    private Dictionary<string, string> loopKeys = new Dictionary<string, string>();


    private float currentBGMTime = 0f;
    private string currentBGMName;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        SetBGMVolume(1.0f);

        DontDestroyOnLoad(this);
    }

    protected override void Awake()
    {

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;

        AddBGM();
        AddSFX();
        AddLoop();
        base.Awake();
    }

    private void Start()
    {
        PlayBGM("IntroBGM");
    }


    public void PlayBGM(string name, bool resume = false)
    {
        if (!bgmKeys.TryGetValue(name, out var addressKey))
        {
            Debug.LogWarning($"BGM을 찾을 수 없음: {name}");
            return;
        }

        Addressables.LoadAssetAsync<AudioClip>(addressKey).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                bgmSource.clip = handle.Result;
                bgmSource.time = resume ? currentBGMTime : 0f;
                currentBGMName = name;
                bgmSource.Play();
            }
            else
            {
                Debug.LogError($"로드 실패: {addressKey}");
            }
        };
    }
    

    public void PlaySFX(string name)
    {
        if (!sfxKeys.TryGetValue(name, out var addressKey))
        {
            Debug.LogWarning($"SFX 찾을 수 없음: {name}");
            return;
        }

        Addressables.LoadAssetAsync<AudioClip>(addressKey).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded &&!sfxSource.isPlaying)
            {
                sfxSource.PlayOneShot(handle.Result);
            }
            else
            {
            }
        };
    }

    public void PlayLoop(string name)
    {
        if (!loopKeys.TryGetValue(name, out var addressKey))
        {
            Debug.LogWarning($"ambience 찾을 수 없음: {name}");
            return;
        }

        Addressables.LoadAssetAsync<AudioClip>(addressKey).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && !loopSource.isPlaying)
            {
                loopSource.clip = handle.Result;
                loopSource.Play();
            }
            else
            {
            }
        };
    }

    public void StopLoop()
    {
        if (loopSource.isPlaying)
        {
            loopSource.Stop();
            loopSource.clip = null;
        }
    }

    private void AddBGM()
    {
        foreach (var bgm in soundLibrary.bgmClips)
        {
            if (!bgmKeys.ContainsKey(bgm.soundName))
            {
                bgmKeys.Add(bgm.soundName, bgm.addressableKey);
            }
        }
    }

    private void AddSFX()
    {
        foreach (var sfx in soundLibrary.sfxClips)
        {
            if (!sfxKeys.ContainsKey(sfx.soundName))
            {
                sfxKeys.Add(sfx.soundName, sfx.addressableKey);
            }
        }
    }

    private void AddLoop()
    {
        foreach (var ambience in soundLibrary.loopClips)
        {
            if (!loopKeys.ContainsKey(ambience.soundName))
            {
                loopKeys.Add(ambience.soundName, ambience.addressableKey);
            }
        }
    }


    public void SetBGMVolume(float volume) => bgmSource.volume = Mathf.Clamp01(volume * 0.3f);
    public void SetSFXVolume(float volume) => sfxSource.volume = volume;

    public float GetBGMVolume() => bgmSource.volume / 0.3f;
    public float GetSFXVolume() => sfxSource.volume;
}
