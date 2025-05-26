using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGatheringManager : GatheringManager
{
    [SerializeField] private TutorialGatheringProps[] gatheringPorps;
    [SerializeField] private GatheringTutorialController gatheringTutorialController;
    public TutorialVideoPlayerController tutorialVideoPlayerController;

    public override void Start()
    {
        SetItem();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        gatheringInventoryUI.LoseAllItem();
    }
}
