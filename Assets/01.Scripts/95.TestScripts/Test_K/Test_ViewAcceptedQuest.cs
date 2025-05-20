using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_ViewAcceptedQuest : MonoBehaviour
{
    public TextMeshProUGUI testQuestSlotPref;
    public Transform testQuestTrs;
    public Button ReviewBtn;
    List<TextMeshProUGUI> isViewingSlots = new();
    private void Awake()
    {
        ReviewBtn.onClick.AddListener(OnClickReview);
    }

    void OnClickReview()
    {
        foreach (var i in isViewingSlots)
        {
            Destroy(i.gameObject);
        }
        isViewingSlots.Clear();

        Quest quest;
        TextMeshProUGUI TMP;
        for (int i = 0; i < QuestManager.Instance.AcceptedQuests.Count; i++)
        {
            quest = Data.GetQuest(QuestManager.Instance.AcceptedQuests[i]);
            TMP = Instantiate(testQuestSlotPref, testQuestTrs, true);
            isViewingSlots.Add(TMP);


            TMP.text = $"퀘스트명 : {quest.Origin.name}\n" +
                $"수락일:{quest.AcceptedDate}\n" +
                $"npc방문일:{quest.TriggerDate}\n" +
                $"퀘스트재발생일:{quest.RecycleDate}\n" +
                $"퀘스트클리어여부:{quest.IsCompletedOnce}\n" +
                $"퀘스트재발생일 지났는지 여부:{quest.RecycleDatePass}\n";
        }
    }
}
