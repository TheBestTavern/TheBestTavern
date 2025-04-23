using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static DesignEnums;

public class ForestGatheringManager : MonoSingleton<ForestGatheringManager>
{
    DesignEnums.Region region;
    DesignEnums.Season season;
    public List<Data_Gathering> data_Gatherings;
    Dictionary<DesignEnums.Chance, List<int>> dict;

    float correction;
    float highGroupProb;
    float mediumGroupProb;
    float lowGroupProb;
    float veryLowGroupProb;

    public GatheringMapController mapController;

    private void Start()
    {
        mapController.CreateMapProps();
        SetItem();
    }

    public void SetItem()
    {
        region = SceneParameter.Get<DesignEnums.Region>("Region");
        season = SceneParameter.Get<DesignEnums.Season>("Season");
        data_Gatherings = DataManager.Instance.DataLoader_Gathering.GetByRegionSeason(region, season, DesignEnums.Biome.forest);
        dict = new();

        // JSON 툴로 들여온 데이터 클래스를 활용
        foreach (var i in data_Gatherings)
        {
            dict.Add(i.condition_chance, i.availableFood);
        }

        // 보정값 계산 및 확률군 별 확률 구하기
        correction = 1 / (0.1f * dict[Chance.veryLow].Count + 0.2f * dict[Chance.low].Count + 0.3f * dict[Chance.medium].Count + 0.4f * dict[Chance.high].Count);
        highGroupProb = 40 * correction * dict[Chance.high].Count;
        mediumGroupProb = 30 * correction * dict[Chance.medium].Count;
        lowGroupProb = 20 * correction * dict[Chance.low].Count;
        veryLowGroupProb = 10 * correction * dict[Chance.veryLow].Count;
    }

    public async void OnMiniGame()
    {
        await SceneLoader.Instance.LoadSceneAsyncMiniGame("Forest_Animal");
        //To Do - 미니게임 열릴때 해줘야하는 것들 (기존 씬에 있는 것들 안보이게 하기)
    }

    public int GetRandomItemID()
    {
        // 랜덤으로 확률군 선정 후 ID 뽑기
        float rand = Random.Range(0, 100);
        int randItemID;

        if (rand < highGroupProb)
        {
            List<int> temp = dict[Chance.high];
            randItemID = temp[Random.Range(0, temp.Count)];
        }
        else if (rand < highGroupProb + mediumGroupProb)
        {
            List<int> temp = dict[Chance.medium];
            randItemID = temp[Random.Range(0, temp.Count)];
        }
        else if (rand < highGroupProb + mediumGroupProb + lowGroupProb)
        {
            List<int> temp = dict[Chance.low];
            randItemID = temp[Random.Range(0, temp.Count)];
        }
        else // 합이 100이 되도록.
        {
            List<int> temp = dict[Chance.veryLow];
            randItemID = temp[Random.Range(0, temp.Count)];
        }

        return randItemID;
    }
}

