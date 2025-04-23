using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ForestGatheringManager : MonoSingleton<ForestGatheringManager>
{
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, .3f);
    [SerializeField] List<Rect> spawnAreas;

    [SerializeField] LayerMask gatheringPropsLayerMask;

    [SerializeField] GameObject[] trees;
    [SerializeField] Transform treeParent;

    [SerializeField] GameObject[] rocks;
    [SerializeField] Transform rockParent;

    [SerializeField] GameObject[] bushs;
    [SerializeField] Transform bushParent;

    [SerializeField] GameObject[] exProps;
    [SerializeField] Transform exPropsParent;

    [SerializeField] GameObject[] fields;
    [SerializeField] Transform fieldParent;

    DesignEnums.Region region;
    DesignEnums.Season season;
    public List<Data_Gathering> data_Gatherings;

    private void Start()
    {
        CreateMapProps();
        SetItem();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

    public void SetItem()
    {
        region = SceneParameter.Get<DesignEnums.Region>("Region");
        season = SceneParameter.Get<DesignEnums.Season>("Season");
        data_Gatherings = DataManager.Instance.DataLoader_Gathering.GetByRegionSeason(region, season, DesignEnums.Biome.forest);
    }

    public void CreateMapProps()
    {
        foreach (var area in spawnAreas)
        {
            int randProps = Random.Range(0, 4);
            // 밭
            if (randProps == 0)
            {
                float x = area.x + area.width / 2;
                float y = area.y + area.height / 2;
                int fieldIdx = Random.Range(0, fields.Length);
                Instantiate(fields[fieldIdx], new Vector3(x, y, 0), Quaternion.identity, fieldParent);
            }
            // 기타 오브젝트
            else
            {
                // 나무
                CreateGatheringProps(area, trees, treeParent, 3, 5);

                // 돌
                CreateEXProps(area, rocks, rockParent, 3, 5);

                // 풀 
                CreateGatheringProps(area, bushs, bushParent, 3, 5);

                // 기타 
                CreateEXProps(area, exProps, exPropsParent, 3, 5);
            }
        }
    }

    void CreateGatheringProps(Rect area, GameObject[] prefabArr, Transform parent, int minCount, int maxCount)
    {
        int randCount = Random.Range(minCount, maxCount);

        for (int i = 0; i < randCount; i++)
        {
            GameObject placed = null;
            int tryLimit = 20;
            int tryCount = 0;

            while (placed == null && tryCount < tryLimit)
            {
                tryCount++;
                float x = Random.Range(area.x, area.x + area.width);
                float y = Random.Range(area.y, area.y + area.height);
                int randPropsIdx = Random.Range(0, prefabArr.Length);
                GameObject prefab = prefabArr[randPropsIdx];

                if (TryPlaceWithoutOverlap(prefab, new Vector2(x, y), gatheringPropsLayerMask, parent, out placed))
                {
                    SetSortingLayer(placed);
                }
            }
        }
    }

    void CreateEXProps(Rect area, GameObject[] prefabArr, Transform parent, int minCount, int maxCount)
    {
        int randCount = Random.Range(minCount, maxCount);
        for (int i = 0; i < randCount; i++)
        {
            float x = Random.Range(area.x, area.x + area.width);
            float y = Random.Range(area.y, area.y + area.height);
            int randIdx = Random.Range(0, prefabArr.Length);
            SetSortingLayer(Instantiate(prefabArr[randIdx], new Vector3(x, y, 0), Quaternion.identity, parent));
        }
    }

    void SetSortingLayer(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = -(int)(obj.transform.position.y * 100);
        }
    }

    bool TryPlaceWithoutOverlap(GameObject prefab, Vector2 position, LayerMask checkMask, Transform parent, out GameObject result)
    {
        result = Instantiate(prefab, position, Quaternion.identity, parent);
        PolygonCollider2D poly = result.GetComponent<PolygonCollider2D>();

        if (poly == null)
        {
            Debug.LogWarning($"[{prefab.name}] 에 PolygonCollider2D가 없습니다.");
            return true;
        }

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(checkMask);
        filter.useTriggers = true;

        List<Collider2D> hits = new();
        int count = poly.OverlapCollider(filter, hits);

        if (count > 0)
        {
            Object.Destroy(result);
            result = null;
            return false;
        }

        return true;
    }

    public async void OnMiniGame()
    {
        await SceneLoader.Instance.LoadSceneAsyncMiniGame("Forest_Animal");
        //To Do - 미니게임 열릴때 해줘야하는 것들 (기존 씬에 있는 것들 안보이게 하기)
    }
}

