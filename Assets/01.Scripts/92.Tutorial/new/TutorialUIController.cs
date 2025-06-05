using UnityEngine;
using UnityEngine.UI;

public class TutorialUIController : MonoBehaviour
{
    ObjectiveUI objectiveUI;
    Animator rope_Animator;
    Button rope_Button;

    public void SetObjectvie()
    {
        objectiveUI.Set();
    }

    public void ActivateRope()
    {
        rope_Animator.SetBool("Active", true);
    }

    public void DeactivateRope()
    {
        rope_Animator.SetBool("Active", false);
    }
}