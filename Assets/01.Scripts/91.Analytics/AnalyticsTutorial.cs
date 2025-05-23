using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsTutorial : Unity.Services.Analytics.Event
{
    public AnalyticsTutorial(string EventName) : base(EventName)
    {
    }

    public bool watchTutorial { set { SetParameter("watchTutorial", value); } }
}
