using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsIntro : Unity.Services.Analytics.Event
{
    public AnalyticsIntro(string name) : base(name)
    {

    }

    public bool watchTutorial { set { SetParameter("watchTutorial", value); } }
}
