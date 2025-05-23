using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelationSlot : MonoBehaviour, IPoolable
{
    Data_NPC npc;
    bool hasMet;

    [SerializeField] TextMeshProUGUI npcnName;
    [SerializeField] TextMeshProUGUI favorability;
    [SerializeField] TextMeshProUGUI taste;
    [SerializeField] List<Image> compensationImages; // 퀘스트 ID와 보상 이미지

    Dictionary<int, string> compensationMap; // 슬롯의 npc가 주는 퀘스트ID와 보상의 영어이름

    public string ID => gameObject.name;

    public bool CanDec => false;

    public float DecPeriod => 0;

    public event Action<IPoolable> OnReturn;

    private RectTransform rect;

    public void Initialize(Action<IPoolable> a)
    {
        OnReturn = a;
        rect = GetComponent<RectTransform>();
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
        npc = null;
        compensationMap.Clear();
        compensationImages.Clear();
    }

    public void OnSpawn(Vector3 pos)
    {
        Debug.Log("출격");
        rect.position = pos;
    }

    public void SetSlot(int npcID)
    {
        // npc등록
        npc = NPCManager.Instance.NPCData.AllNPC[npcID].Origin;

        // 보상 정보 미리 등록( 주는 퀘스트 리스트에 대응하는 영어 이름 등록하기.)
        compensationMap = new();
        for (int j = 0; j < npc.givingQuest.Count; j++)
        {
            int compensationID = DataManager.Instance.DataLoader_Quest.ItemsDict[npc.givingQuest[j]].compensationID;
            string compensationName = Data.GetRawItem(compensationID).englishName;
            compensationMap[npc.givingQuest[j]] = compensationName;
        }

        // 안쓰는 보상아이템 이미지는 끄기.
        int i = 0;
        for (; i < compensationMap.Count; i++)
        {
            compensationImages[i].gameObject.SetActive(true);
        }
        for (; i < compensationImages.Count; i++)
        {
            compensationImages[i].gameObject.SetActive(false);
        }

        // 초기 상태
        if (Data.GetNPC(npc.key).HasMet)
        {
            hasMet = true;
        }
        else
        {
            favorability.text = "";
            npcnName.text = "";
            taste.text = "";
        }

        if (hasMet)
        {
            UpdateBasicInfo();
            UpdateFavor();
            UpdateSuccessQuest();
        }
    }

    public void TriggerReturn()
    {
        OnReturn?.Invoke(this);
    }

    public void UpdateHasMet()
    {
        hasMet = true;
        UpdateBasicInfo();
        UpdateFavor();
        UpdateSuccessQuest();
    }

    public void UpdateBasicInfo()
    {
        npcnName.text = npc.name;
        taste.text = npc.taste.ToString();
    }

    public void UpdateFavor()
    {
        favorability.text = Data.GetNPC(npc.key).Favorability.ToString();
    }

    public async void UpdateSuccessQuest()
    {
        int i = 0;
        foreach (var pair in compensationMap)
        {
            QuestManager.Instance.OnceSuccessQuests.TryGetValue(pair.Key, out var successDegree);
            if ((int)successDegree >= 20)
            {
                compensationImages[i].sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", pair.Value, true);
                i++;
            }
        }
        for (; i < compensationImages.Count; i++)
        {
            compensationImages[i].sprite = await AddressablesLoader.Instance.AddressablesLoadSpriteFromAtlasAsync("FoodSpriteAtlas", "Undiscovered2", true);
        }
    }
}