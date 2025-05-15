using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;

public class GatheringManager : MonoSingleton<GatheringManager>
{
    public RectTransform invenRect;
    public RectTransform canvsRect;
    public Camera uiCamera;

    DesignEnums.RegionType region;
    DesignEnums.SeasonType season;
    protected DesignEnums.BiomeType biome;

    public List<Data_Gathering> data_Gatherings;
    Dictionary<DesignEnums.ChanceType, List<int>> itemDict;

    float correction;
    float highGroupProb;
    float mediumGroupProb;
    float lowGroupProb;
    float veryLowGroupProb;

    public GatheringInventoryUI gatheringInventoryUI;

    [SerializeField] private GatheringMapController gatheringMapController;

    public virtual void Start()
    {
        gatheringMapController.CreateMapProps();
        SetItem();
    }

    public void SetItem()
    {
        region = SceneParameter.Get<DesignEnums.RegionType>("Region");
        season = SceneParameter.Get<DesignEnums.SeasonType>("Season");
        data_Gatherings = Data.GetByRegionSeasonBiome(region, season, biome);

        itemDict = new();

        //JSON 툴로 들여온 데이터 클래스를 활용
        foreach (var i in data_Gatherings)
        {
            itemDict.Add(i.condition_chance, i.availableFood);
        }

        // 보정값 계산 및 확률군 별 확률 구하기
        correction = 1 / (0.1f * itemDict[ChanceType.veryLow].Count + 0.2f * itemDict[ChanceType.low].Count + 0.3f * itemDict[ChanceType.medium].Count + 0.4f * itemDict[ChanceType.high].Count);
        highGroupProb = 40 * correction * itemDict[ChanceType.high].Count;
        mediumGroupProb = 30 * correction * itemDict[ChanceType.medium].Count;
        lowGroupProb = 20 * correction * itemDict[ChanceType.low].Count;
        veryLowGroupProb = 10 * correction * itemDict[ChanceType.veryLow].Count;
    }

    public async void OnMiniGame(string miniGameName)
    {
        await SceneLoader.Instance.LoadSceneAsyncMiniGame(miniGameName);
        //To Do - 미니게임 열릴때 해줘야하는 것들 (기존 씬에 있는 것들 안보이게 하기)
    }

    public int GetRandomItemID()
    {
        // 랜덤으로 확률군 선정 후 ID 뽑기
        float rand = Random.Range(0, 100);
        int randItemID;

        if (rand < highGroupProb)
        {
            List<int> temp = itemDict[ChanceType.high];
            randItemID = temp[Random.Range(0, temp.Count)];
        }
        else if (rand < highGroupProb + mediumGroupProb)
        {
            List<int> temp = itemDict[ChanceType.medium];
            randItemID = temp[Random.Range(0, temp.Count)];
        }
        else if (rand < highGroupProb + mediumGroupProb + lowGroupProb)
        {
            List<int> temp = itemDict[ChanceType.low];
            randItemID = temp[Random.Range(0, temp.Count)];
        }
        else // 합이 100이 되도록.
        {
            List<int> temp = itemDict[ChanceType.veryLow];
            randItemID = temp[Random.Range(0, temp.Count)];
        }

        return randItemID;
    }
}
