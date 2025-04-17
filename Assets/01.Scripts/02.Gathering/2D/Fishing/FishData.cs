using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fishing/Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public Sprite fishSprite;
    public float difficulty;
    public float value;
}
