using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour, IPoolable
{
    Quest quest;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI submissionDate;
    [SerializeField] Image compensationImage;
    [SerializeField] Button letterBtn;
    bool isInProgressQuest;

    public string ID => gameObject.name;

    public bool CanDec => false;

    public float DecPeriod => 0;

    public event Action<IPoolable> OnReturn;

    public async void SetSlot(int questID, bool isInProgressQuest)
    {
        quest = Data.GetQuest(questID);
        npcName.text = Data.GetNPC(quest.Origin.givingNPC).Origin.name;
        questName.text = quest.Origin.name;
        this.isInProgressQuest = isInProgressQuest;
        if (isInProgressQuest)
        {
            submissionDate.text = quest.TriggerDate.ToString();
            submissionDate.gameObject.SetActive(true);
            compensationImage.gameObject.SetActive(false);
        }
        else
        {
            string englishName = Data.GetRawItem(quest.Origin.compensationID).englishName;
            compensationImage.sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", englishName, true);

            submissionDate.gameObject.SetActive(false);
            compensationImage.gameObject.SetActive(true);
        }
    }

    public void Initialize(Action<IPoolable> a)
    {
        OnReturn = a;
        letterBtn.onClick.AddListener(OnClickLetterBtn);
    }

    public void OnClickLetterBtn()
    {
        EventBus.Publish<QuestClickLetterBtnEvent>(new QuestClickLetterBtnEvent(quest, isInProgressQuest));
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public void OnSpawn(Vector3 pos)
    {
        Debug.Log("출격");
    }

    public void TriggerReturn()
    {
        OnReturn?.Invoke(this);
    }
}
