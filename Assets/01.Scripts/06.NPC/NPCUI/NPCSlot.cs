using UnityEngine;
using UnityEngine.UI;

public class NPCSlot : MonoBehaviour
{
    NPCArea npcArea;
    Quest quest;
    [SerializeField] Transform messagePivot;
    [SerializeField] Image image;
    [SerializeField] Button btn;

    public void Init()
    {
        btn.onClick.AddListener(OnClickBtn);
        transform.gameObject.SetActive(false);
    }

    public void SetSlot(Quest quest, NPCArea npcArea)
    {
        image.sprite = Resources.Load<Sprite>("NPC/"+ NPCManager.Instance.AllNPC[quest.origin.givingNPC].origin.name);
        this.npcArea = npcArea;
        this.quest = quest;
    }

    private void OnClickBtn()
    {
        npcArea.EnterQuestSubmissionMode(quest, this);
    }
}