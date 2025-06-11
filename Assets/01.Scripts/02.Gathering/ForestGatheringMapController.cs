using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ForestGatheringMapController : GatheringMapController
{
    [SerializeField] string treesLabelName = "Tree";
    [SerializeField] string bushLabelName = "Bush";
    [SerializeField] string fieldLabelName = "Field";

    List<GameObject> trees;
    List<GameObject> bushes;
    List<GameObject> fields;

    public async override void CreateMapProps()
    {
        await LoadProps();
        int fieldIdx = Random.Range(0, fields.Count);
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
        int randTreeIdx = Random.Range(0, trees.Count);
        SetSortingLayer(Instantiate(trees[randTreeIdx], new Vector3(x, y, 0), Quaternion.identity, propsParent));

        int randBushIdx = Random.Range(0, bushes.Count);
        int randBushCount = Random.Range(0, 3);
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

    public async override Task LoadProps()
    {
        trees = await AddressablesLoader.Instance.AddressablesListLoadFromLabelAsync(treesLabelName);
        bushes = await AddressablesLoader.Instance.AddressablesListLoadFromLabelAsync(bushLabelName);
        fields = await AddressablesLoader.Instance.AddressablesListLoadFromLabelAsync(fieldLabelName);
    }
}
