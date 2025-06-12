using UnityEngine;
using UnityEngine.UI;

public class TtrUI : MonoBehaviour
{
    TutorialManager manager;

    [SerializeField] ObjectiveUI objectiveUI;
    [SerializeField] Animator rope_Animator;
    [SerializeField] Button rope_Button;
    [SerializeField] ConversationUI conversationUI;

    public void Init(TutorialManager manager)
    {
        rope_Button.onClick.AddListener(OnClickRope);
        this.manager = manager;
    }

    /// <summary>
    /// conversationUI 컨트롤
    /// </summary>
    public void StartConversation(int TutorialStepID)
    {
        conversationUI.StartConversation(TutorialStepID);
    }

    /// <summary>
    /// objectiveUI 컨트롤
    /// </summary>
    public void SetAllObvs()
    {
        objectiveUI.SetAll();
    }

    public void SetObv(int index)
    {
        objectiveUI.SetObv(index);
    }

    public void HideObjectvie()
    {
        objectiveUI.Hide();
    }

    /// <summary>
    /// RopeUI 컨트롤
    /// </summary>
    public void ActivateRope()
    {
        rope_Animator.SetBool("Active", true);
        rope_Button.enabled = true;
    }

    public void DeactivateRope()
    {
        rope_Animator.SetBool("Active", false);
        rope_Button.enabled = false;
    }

    public void OnClickRope()
    {
        if (manager.GetCurTtrStepDef() != null)
            manager.ClearStep();
        StartConversation(manager.nextStepID);
    }

    private void OnDestroy()
    {
        rope_Button.onClick.RemoveAllListeners();
    }
}