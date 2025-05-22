using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class AnalyticsItem : Unity.Services.Analytics.Event
{
    public AnalyticsItem(string EventName) : base(EventName)
    {

    }

    public string ItemName { set { SetParameter("ItemName", value); } }
}
