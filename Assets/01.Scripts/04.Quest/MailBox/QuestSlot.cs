using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI questName;

    [SerializeField] Button openBtn;

    public void SetSlot(Quest quest)
    {
        // 현재 퀘스트에 맞게 슬롯 정보 갱신
        // (추후 구현)

        // 버튼에 메서드 구독
        openBtn.onClick.RemoveAllListeners();
        openBtn.onClick.AddListener(() => OpenLetter(quest));
    }

    private void OpenLetter(Quest quest)
    {
        QuestManager.Instace.AcceptQuest(quest);

    }
}