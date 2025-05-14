using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GatheringSea : GatheringProps
{
    protected override void OnClickedFunc()
    {
        UIManager.Instance.gatheringSceneUI.SetMiniGameBackGround(true);
        ForestGatheringManager.Instance.OnMiniGame("Ocean_Fishing");
    }
}
