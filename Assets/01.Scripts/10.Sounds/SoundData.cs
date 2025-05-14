using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundData", menuName = "Sound/SoundData")]
public class SoundData : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
}
