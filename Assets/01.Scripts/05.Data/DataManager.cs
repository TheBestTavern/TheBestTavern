using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public Data_Gathering_BiomeLoader DataLoader_Gathering_Biome { get; private set; } // 생태별 채집할 수 있는 아이템 정보
    //public Data_Gathering_ChanceLoader DataLoader_Gathering_Chance { get; private set; } // 획득 확률(낮음,보통,높음, 매우낮음)별 해당하는 아이템 정보
    //public Data_Gathering_RegionLoader DataLoader_Gathering_Region { get; private set; } // 지역별 채집할 수 있는 아이템 정보
    //public Data_Gathering_SeasonLoader DataLoader_Gathering_Season { get; private set; } // 계절별 채집할 수 있는 아이템 정보
    public Data_GatheringLoader DataLoader_Gathering { get; private set; } // 계절별 채집할 수 있는 아이템 정보
    public Data_NPCLoader DataLoader_NPC { get; private set; } // NPC 정보 (부여 아이템, 이름, 초기 호감도 등)
    public Data_QuestLoader DataLoader_Quest { get; private set; } // 퀘스트 정보 (퀘스트 등장 조건, 부여 npc 등)

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    // 데이터 로더 인스턴스 생성.
    public void Init()
    {
        DataLoader_CookingSteps = new();
        DataLoader_Foods = new();
        //DataLoader_Gathering_Biome = new();
        //DataLoader_Gathering_Chance = new();
        //DataLoader_Gathering_Region = new();
        //DataLoader_Gathering_Season = new();
        DataLoader_Gathering = new();
        DataLoader_NPC = new();
        DataLoader_Quest = new();
    }
}
