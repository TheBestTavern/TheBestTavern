using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class TtrStepInstance
{
    public int ttrStepDefID;
    public TtrInstanceState instanceState;
    public List<int> obvCurCounts;
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