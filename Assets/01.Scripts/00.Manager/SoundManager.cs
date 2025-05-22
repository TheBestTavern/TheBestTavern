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
    [SerializeField] private AudioSource ambienceSource;



    [Header("Audio Clips")]
    [SerializeField] private SoundLibrary soundLibrary;
    private Dictionary<string, string> bgmKeys = new Dictionary<string, string>();
    private Dictionary<string, string> sfxKeys = new Dictionary<string, string>();
    private Dictionary<string, string> ambienceKeys = new Dictionary<string, string>();


    private float currentBGMTime = 0f;
    private string currentBGMName;

    public async  override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);

        if (soundLibrary == null)
            soundLibrary = await AddressablesLoader.Instance.AddressablesLoadAsync<SoundLibrary>("SoundLibrary.SO");

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        ambienceSource = gameObject.AddComponent<AudioSource>();
        ambienceSource.loop = true;

        AddBGM();
        AddSFX();
        AddAmbience();

        SetBGMVolume(1.0f);
        EventBus.Subscribe<EndNightUIBlockEvent>((evt) => PlaySFX("Kokkio"));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventBus.UnSubscribe<EndNightUIBlockEvent>((evt) => PlaySFX("Kokkio"));
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
            if (handle.Status == AsyncOperationStatus.Succeeded && !sfxSource.isPlaying)
            {
                sfxSource.PlayOneShot(handle.Result);
            }
            else
            {
            }
        };
    }

    public void PlayAmbience(string name)
    {
        if (!ambienceKeys.TryGetValue(name, out var addressKey))
        {
            Debug.LogWarning($"ambience 찾을 수 없음: {name}");
            return;
        }

        Addressables.LoadAssetAsync<AudioClip>(addressKey).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && !ambienceSource.isPlaying)
            {
                ambienceSource.clip = handle.Result;
                ambienceSource.Play();
            }
            else
            {
            }
        };
    }

    public void StopLoop()
    {
        if (ambienceSource.isPlaying)
        {
            ambienceSource.Stop();
            ambienceSource.clip = null;
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

    private void AddAmbience()
    {
        foreach (var ambience in soundLibrary.ambienceClips)
        {
            if (!ambienceKeys.ContainsKey(ambience.soundName))
            {
                ambienceKeys.Add(ambience.soundName, ambience.addressableKey);
            }
        }
    }


    public void SetBGMVolume(float volume) => bgmSource.volume = Mathf.Clamp01(volume * 0.3f);
    public void SetSFXVolume(float volume) => sfxSource.volume = volume;

    public float GetBGMVolume() => bgmSource.volume / 0.3f;
    public float GetSFXVolume() => sfxSource.volume;
}
