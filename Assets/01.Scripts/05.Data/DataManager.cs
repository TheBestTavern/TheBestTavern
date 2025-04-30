using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Data // 간단 key 검색 클래스
{
    public static Quest GetQuest(int id)
    {
        if (QuestManager.Instance.AllQuests.TryGetValue(id, out Quest quest))
        {
            return quest;
        }
        else
        {
            Debug.LogWarning("없는 퀘스트 id입니다");
            return null;
        }
    }
    //public static Item GetItem(int id)
    //{
    //    if (ItemManager.Instance.AllItems.TryGetValue(id, out Item item))
    //    {
    //        return item;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("없는 아이템 id입니다");
    //        return null;
    //    }
    //}

    public static Data_Foods GetRawItem(int key)
    {
        return DataManager.Instance.DataLoader_Foods.GetByKey(key);
    }

    public static NPC GetNPC(int id)
    {
        if (NPCManager.Instance.AllNPC.TryGetValue(id, out NPC npc))
        {
            return npc;
        }
        else
        {
            Debug.LogWarning("없는 엔피씨 id입니다");
            return null;
        }
    }

    public static List<Data_Gathering> GetByRegionSeasonBiome(DesignEnums.RegionType region, DesignEnums.SeasonType season, DesignEnums.BiomeType biome)
    {
        return DataManager.Instance.DataLoader_Gathering.ItemsList.Where(item =>
        item.condition_region == region &&
        item.condition_season == season &&
        item.condition_biome == biome).ToList();
    }
}

/// <summary>
/// - 임시 데이터(최종 아님) 
/// - 구조,형태 달라질 수도 있음. 
/// - 요리 레시피 데이터는 아직 추가되지 않음.
/// - 퀘스트 데이터에 타겟 요리 변수가 아직 추가되지 않음.
/// </summary>
public class DataManager : MonoSingleton<DataManager>
{
    public Data_CookingStepsLoader DataLoader_CookingSteps { get; private set; } // 요리 방식 데이터 ex.끓이기 찌기, 굽기 등
    public Data_FoodsLoader DataLoader_Foods { get; private set; } // 게임 상에 존재하는 모든 아이템(재료, 가공재료, 요리 포함)
    public Data_FoodCategoryLoader DataLoader_FoodCategory { get; private set; } // 게임 상에 존재하는 모든 아이템(재료, 가공재료, 요리 포함)
    public Data_GatheringLoader DataLoader_Gathering { get; private set; } // 계절별 채집할 수 있는 아이템 정보
    public Data_NPCLoader DataLoader_NPC { get; private set; } // NPC 정보 (부여 아이템, 이름, 초기 호감도 등)
    public Data_QuestLoader DataLoader_Quest { get; private set; } // 퀘스트 정보 (퀘스트 등장 조건, 부여 npc 등)
    public Data_RecipesLoader Dataloader_Recipes { get; private set; } // 퀘스트 정보 (퀘스트 등장 조건, 부여 npc 등)

    // 데이터 로더 인스턴스 생성.
    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DataLoader_CookingSteps = new();
        DataLoader_Foods = new();
        DataLoader_Gathering = new();
        DataLoader_NPC = new();
        DataLoader_Quest = new();
        Dataloader_Recipes = new();
        DataLoader_FoodCategory = new();
    }
}
