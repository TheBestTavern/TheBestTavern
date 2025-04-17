using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBait", menuName = "Bait/BaitData")]
public class BaitData : ScriptableObject
{
    public string baitType; // 예: "Berry", "Meat"
    public Sprite icon;
    public GameObject baitPrefab;
}
