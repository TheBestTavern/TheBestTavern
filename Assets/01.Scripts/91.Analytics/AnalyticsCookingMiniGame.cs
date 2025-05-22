using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsCookingMiniGame : Unity.Services.Analytics.Event
{
    public AnalyticsCookingMiniGame(string EventName) : base(EventName)
    {
    }

    public string miniGameName { set { SetParameter("miniGameName", value); } }
}
