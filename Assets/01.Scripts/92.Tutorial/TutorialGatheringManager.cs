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

    public void CheckAllClicked()
    {
        foreach(var prop in gatheringPorps)
        {
            if (!prop.isClickedUsedTutorial)
            {
                return;
            }
        }

        gatheringTutorialController.NextButton.gameObject.SetActive(true);
    }
}
