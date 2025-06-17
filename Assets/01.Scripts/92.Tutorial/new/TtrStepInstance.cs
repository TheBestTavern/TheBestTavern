using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class TtrStepInstance
{
    [JsonProperty]
    public int ttrStepDefID;
    [JsonProperty]
    public TtrInstanceState instanceState;
    [JsonProperty]
    public List<int> obvCurCounts;
    [JsonProperty]
    public List<ObvState> obvStates;
    public TtrStepInstance(int ttrStepDefID, int obvCount)
    {
        this.ttrStepDefID = ttrStepDefID;
        instanceState = TtrInstanceState.InProgress;
        obvStates = new();
        obvCurCounts = new();
        for (int i = 0; i < obvCount; i++)
        {
            obvStates.Add(ObvState.InProgress);
            obvCurCounts.Add(0);
        }
    }
}