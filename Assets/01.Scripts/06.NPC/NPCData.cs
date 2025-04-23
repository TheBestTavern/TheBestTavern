using UnityEngine;
using System.Collections.Generic;

public class NPCData
{
    public Dictionary<int, NPC> AllNPC { get; private set; } = new();

    public void Init()
    {
        Debug.Log("NPC 인스턴스 생성");
        foreach (Data_NPC item in DataManager.Instance.DataLoader_NPC.ItemsList)
        {
            AllNPC.Add(item.key, new NPC(item));
        }
    }
}