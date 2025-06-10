using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class TtrStepInstance
{
    public int ttrStepDefID;
    public int curCount;
    public TtrInstanceState instanceState;
    public List<ObvState> ObvsStates;
    public TtrStepInstance(int ttrStepDefID)
    {
        this.ttrStepDefID = ttrStepDefID;
        instanceState = TtrInstanceState.InProgress;
        ObvsStates = new();
    }
}