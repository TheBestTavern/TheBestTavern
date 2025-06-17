using UnityEngine;
using UnityEngine.UI;

public class TtrUI : MonoBehaviour
{
    TutorialManager manager;

    [SerializeField] ObjectiveUI objectiveUI;
    [SerializeField] Animator rope_Animator;
    [SerializeField] Button rope_Button;
    [SerializeField] Image rope_Image;
    [SerializeField] ChatUI chatUI;

    [SerializeField] Button DevBtn;


    public async void Init(TutorialManager manager)
    {
        rope_Button.onClick.AddListener(OnClickRope);
        this.manager = manager;
#if UNITY_EDITOR
        DevBtn.onClick.AddListener(() => manager.Progress2ReadyClearStep());
#else
        DevBtn.gameObject.SetActive(false);
#endif

        await DayAndNightManager.Instance.InitAsync();
        rope_Image.material = DayAndNightManager.Instance.nightMat;
    }

    /// <summary>
    /// conversationUI 컨트롤
    /// </summary>
    public void StartConversation(int? ttrID)
    {
        chatUI.StartConversation(ttrID);
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
        DeactivateRope();
        StartConversation(manager.curStepID);
    }

    private void OnDestroy()
    {
        rope_Button.onClick.RemoveAllListeners();
    }
}