using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGatheringMapController : GatheringMapController
{
    [SerializeField] GameObject[] trees;
    [SerializeField] GameObject[] bushes;
    [SerializeField] GameObject[] fields;

    public override void CreateMapProps()
    {
        int fieldIdx = Random.Range(0, fields.Length);
        foreach (var area in spawnAreas)
        {
            int randProps = Random.Range(0, 4);
            // 밭
            if (randProps == 0)
            {
                CreateFields(area, fieldIdx);
            }
            else
            {
                CreateTreesAndBushes(area);
            }
        }
    }

    void CreateFields(Rect area, int idx)
    {
        float x = area.x + area.width / 2;
        float y = area.y + area.height / 2;
        Instantiate(fields[idx], new Vector3(x, y, 0), fields[idx].transform.rotation, propsParent);
    }

    void CreateTreesAndBushes(Rect area)
    {
        float x = area.x + area.width / 2;
        int randY = Random.Range(0, 2);
        float y;
        if (randY == 0)
        {
            y = area.y;
        }
        else
        {
            y = area.y + area.height;
        }
        int randTreeIdx = Random.Range(0, trees.Length);
        SetSortingLayer(Instantiate(trees[randTreeIdx], new Vector3(x, y, 0), Quaternion.identity, propsParent));

        int randBushIdx = Random.Range(0, bushes.Length);
        int randBushCount = Random.Range(0, 4);
        int half = randBushCount / 2;
        float bushY = y - 1;
        for (int i = -half; i <= half; i++)
        {
            if (randBushCount % 2 == 0 && i == 0)
                continue;

            float bushX = x + i * 1.5f;
            SetSortingLayer(Instantiate(bushes[randBushIdx], new Vector3(bushX, bushY, 0), Quaternion.identity, propsParent));
        }
    }
}
