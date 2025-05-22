using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsQuest : Unity.Services.Analytics.Event
{
    public AnalyticsQuest(string EventName) : base(EventName)
    {
    }
    
    public string questName { set { SetParameter("questName", value); } }
}
