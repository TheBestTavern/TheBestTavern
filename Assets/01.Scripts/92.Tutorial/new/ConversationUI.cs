using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    [SerializeField] Button NextPage;
    [SerializeField] Button ReceiveStep;
    [SerializeField] Button CancelTutorial;

    int ConversationTtrID;

    private void Awake()
    {
        ReceiveStep.onClick.AddListener(() => { TutorialManager.Instance.AcceptNewStep(ConversationTtrID); EndConversation(); });
        CancelTutorial.onClick.AddListener(async () => { 
            await PopUpManager.Instance.ShowPopUp(PopUpType.Confirm);
            PopUpManager.Instance.confirmPopUp.SetConfirm("한번 튜토리얼을 취소하면 다시 받을 수 없습니다. \n 정말 튜토리얼을 취소하시겠습니까? ", () => TutorialManager.Instance.CancelTutorial());
        });
    }

    public void StartConversation(int ConversationTtrID)
    {
        this.ConversationTtrID = ConversationTtrID;
        gameObject.SetActive(true);

        // todo 대화 로직 시작
    }

    public void EndConversation()
    {
        gameObject.SetActive(false);

    }
}