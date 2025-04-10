using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Bait")]
public class BaitItem : ScriptableObject
{
    public string baitName;
    public float captureChance = 0.3f; // 기본 포획 확률
}
