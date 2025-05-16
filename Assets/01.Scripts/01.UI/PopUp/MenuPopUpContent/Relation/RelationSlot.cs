using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelationSlot : MonoBehaviour, IPoolable
{
    Data_NPC npc;

    TextMeshProUGUI npcnName;
    TextMeshProUGUI favorability;
    TextMeshProUGUI taste;

    bool hasMet;

    List<Image> compensationImages; // 퀘스트 ID와 보상 이미지
    //Dictionary<int, bool> questsClear= new(); // npc가 주는 퀘스트ID와 보상의 이름
    Dictionary<int, string> compensationMap = new(); // npc가 주는 퀘스트ID와 보상의 이름

    public string ID => gameObject.name;

    public bool CanDec => false;

    public float DecPeriod => 0;

    public event Action<IPoolable> OnReturn;

    public RectTransform rect;

    public void Initialize(Action<IPoolable> a)
    {
        OnReturn = a;
        rect = GetComponent<RectTransform>();
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public void OnSpawn(Vector3 pos)
    {
        Debug.Log("출격");
        rect.position = pos;
    }

    public void SetSlot(int npcID)
    {
        npc = NPCManager.Instance.NPCData.AllNPC[npcID].origin;
        npcnName.text = npc.name;
        taste.text = npc.taste.ToString();
        foreach (var pair in DataManager.Instance.DataLoader_Quest.ItemsDict)
        {
            if (pair.Value.givingNPC == npc.key)
            {
                compensationMap[pair.Key] = Data.GetRawItem(pair.Value.compensationID).englishName;
            }
        }

        UpdateSlot();
    }

    public void TriggerReturn()
    {
        OnReturn?.Invoke(this);
    }

    public async void UpdateSlot()
    {
        int i = 0;
        foreach (var pair in compensationMap)
        {
            QuestManager.Instance.OnceCompletedQuests.TryGetValue(pair.Key, out var successDegree);
            if((int)successDegree >= 10)
            {
                compensationImages[i].sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>($"Assets/16.Image/FoodImage/{Data.GetRawItem(pair.Key).englishName}.png", true);
            }
            i++;
        }
        for(; i < compensationImages.Count; i++)
        {
            compensationImages[i].sprite = await AddressablesLoader.Instance.AddressablesLoadAsync<Sprite>("default.Sprite");
        }

        favorability.text = npc.favorability.ToString();
    }

    public void UpdateFavor()
    {

    }

    public void UpdateFirstMet()
    {

    }

    public void UpdateSuccessQuest()
    {

    }
}