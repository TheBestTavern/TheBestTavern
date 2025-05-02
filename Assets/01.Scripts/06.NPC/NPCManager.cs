using System;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : MonoSingleton<NPCManager>
{
    public NPCData NPCData;
    public Action onNewDayStarted;
    public Dictionary<int, NPC> AllNPC => NPCData.AllNPC;


    public override void Init()
    {
        if(_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);

        NPCData = new NPCData();
        NPCData.Init();
    }
}