using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    [SerializeField] Button NextPage;
    [SerializeField] Button ReceiveQuest;

    int ConversationTtrID;

    private void Awake()
    {
        ReceiveQuest.onClick.AddListener(() => TutorialManager.Instance.ChangeCurTutorial(ConversationTtrID));
    }

    public void StartConversation(int ConversationTtrID)
    {
        this.ConversationTtrID = ConversationTtrID;

        // todo 대화 로직 시작
    }
}