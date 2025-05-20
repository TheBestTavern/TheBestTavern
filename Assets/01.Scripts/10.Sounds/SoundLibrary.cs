using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Sound/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    public SoundData[] bgmClips;
    public SoundData[] sfxClips;
    public SoundData[] ambienceClips;
}
