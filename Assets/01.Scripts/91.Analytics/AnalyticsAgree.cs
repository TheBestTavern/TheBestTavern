using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsAgree : Unity.Services.Analytics.Event
{
    public AnalyticsAgree(string EventName) : base(EventName)
    {
    }

    public bool agreeAnalytics { set { SetParameter("agreeAnalytics", value); } }
}
