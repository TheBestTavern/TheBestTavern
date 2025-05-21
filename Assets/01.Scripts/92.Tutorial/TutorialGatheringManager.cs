using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGatheringManager : GatheringManager
{
    [SerializeField] private TutorialGatheringProps[] gatheringPorps;
    [SerializeField] private GatheringTutorialController gatheringTutorialController;

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

        OnMiniGame("Forest_Animal_Dev");
        gatheringTutorialController.OnClickNextButton();
        gatheringTutorialController.NextButton.gameObject.SetActive(true);
    }
}
