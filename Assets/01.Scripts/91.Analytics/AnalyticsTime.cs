using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsTime : Unity.Services.Analytics.Event
{
    public AnalyticsTime(string eventName) : base(eventName)
    {
    }

    public string dateData { set { SetParameter("dateData", value); } }
}
