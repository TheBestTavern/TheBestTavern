using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsTime : Unity.Services.Analytics.Event
{
    public AnalyticsTime(string eventName) : base(eventName)
    {
    }

    public int year { set { SetParameter("year", value); } }
    public int month { set { SetParameter("month", value); } }
    public int day { set { SetParameter("day", value); } }
}
