using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringSea : GatheringProps
{
    protected override void OnClickedFunc()
    {
        ForestGatheringManager.Instance.OnMiniGame("Ocean_Fishing");
    }
}
