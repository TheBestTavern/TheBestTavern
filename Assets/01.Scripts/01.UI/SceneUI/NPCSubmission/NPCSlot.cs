using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCSlot : MonoBehaviour
{
    public int index; // 0부터 시작
    NPCArea npcArea;
    Quest quest;
    NPC npc;
    [SerializeField] Transform messagePivot;
    [SerializeField] Image image;
    [SerializeField] Button btn;
    [SerializeField] TextMeshProUGUI TmpMessage;

    public void Init(int index, NPCArea npcArea)
    {
        this.index = index;
        this.npcArea = npcArea;
        btn.onClick.AddListener(OnClickBtn);
        transform.gameObject.SetActive(false);
        //Debug.Log("슬롯의 켜짐상태:" + gameObject.activeSelf); // 테스트용
        messagePivot.gameObject.SetActive(false);
    }

    public void SetSlot(int questID)
    {
        this.quest = Data.GetQuest(questID);
        npc = NPCManager.Instance.AllNPC[quest.origin.givingNPC];
        image.sprite = Resources.Load<Sprite>("NPC/" + npc.origin.name);
        TmpMessage.text = npc.origin.thanksMent;
    }

    private void OnClickBtn()
    {
        npcArea.EnterQuestSubmissionMode(quest, this);
        npc.Meet();
    }

    public void ShowMessage()
    {
        messagePivot.gameObject.SetActive(true);
    }

    public void OnExit()
    {
        HideMessage();
        transform.SetParent(npcArea.transform);
        transform.localPosition = Vector3.zero;
    }

    void HideMessage()
    {
        messagePivot.gameObject.SetActive(false);
    }
}