using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TutorialGatheringProps : GatheringBase
{
    public bool isClickedUsedTutorial = false;

    protected override void OnClickedFunc()
    {
        base.OnClickedFunc(); 
        transform.DOShakeRotation(1f, 1f);
        OffMouseFunc();
        spriteRenderer.DOColor(new Color(0.5f, 0.5f, 0.5f), 0.5f);
        isClickedUsedTutorial = true;

        TutorialGatheringManager tutorialGatheringManager = GatheringManager.Instance as TutorialGatheringManager;
        tutorialGatheringManager.CheckAllClicked();
    }
}
