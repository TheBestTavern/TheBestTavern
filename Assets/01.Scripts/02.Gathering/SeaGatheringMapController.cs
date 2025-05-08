using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SeaGatheringMapController : GatheringMapController
{
    [SerializeField] GameObject[] rocks;
    [SerializeField] GameObject[] holes;

    public override void CreateMapProps()
    {
        foreach (var area in spawnAreas)
        {
            int rand = Random.Range(0, 5);
            if (rand == 0)
            {
                CreatRocks(area);
            }
            else
            {
                CreateHole(area);
            }
        }
    }

    private void CreateHole(Rect rect)
    {
        int randholeCount = Random.Range(1, 3);
        int half = randholeCount / 2;
        float x = rect.x + rect.width / 2;
        float holeY = rect.y + rect.height / 2;
        int holeIdx = Random.Range(0, holes.Length);

        for (int i = -half; i <= half; i++)
        {
            if (randholeCount % 2 == 0 && i == 0)
                continue;

            float holeX = x + i * 1.5f;
            SetSortingLayer(Instantiate(holes[holeIdx], new Vector3(holeX, holeY, 0), Quaternion.identity, propsParent));
        }
    }

    void CreatRocks(Rect rect)
    {
        int randRockCount = 1;
        int half = randRockCount / 2;
        float x = rect.x + rect.width / 2;
        float rockY = rect.y + rect.height / 2;
        int rockIdx = Random.Range(0, rocks.Length);

        for (int i = -half; i <= half; i++)
        {
            if (randRockCount % 2 == 0 && i == 0)
                continue;

            float rockX = x + i * 1.5f;
            SetSortingLayer(Instantiate(rocks[rockIdx], new Vector3(rockX, rockY, 0), Quaternion.identity, propsParent));
        }
    }
}
