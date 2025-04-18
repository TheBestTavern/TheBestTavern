using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringManager : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, .3f);
    [SerializeField] List<Rect> spawnAreas;

    [SerializeField] GameObject[] trees;
    [SerializeField] Transform treeParent;

    [SerializeField] GameObject[] rocks;
    [SerializeField] Transform rockParent;

    [SerializeField] GameObject[] bushs;
    [SerializeField] Transform bushParent;


    [SerializeField] GameObject[] fields;
    [SerializeField] Transform fieldParent;

    private void Awake()
    {
        CreateMapProps();
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
                int randTreeCount = Random.Range(3, 5);
                for (int i = 0; i < randTreeCount; i++)
                {
                    float x = Random.Range(area.x, area.x + area.width);
                    float y = Random.Range(area.y, area.y + area.height);
                    int randTreeIdx = Random.Range(0, trees.Length);
                    Instantiate(trees[randTreeIdx], new Vector3(x, y, 0), Quaternion.identity, treeParent);
                }
                // 돌
                int randRockCount = Random.Range(3,5);
                for (int i = 0; i < randRockCount; i++)
                {
                    float x = Random.Range(area.x, area.x + area.width);
                    float y = Random.Range(area.y, area.y + area.height);
                    int randTreeIdx = Random.Range(0, rocks.Length);
                    Instantiate(rocks[randTreeIdx], new Vector3(x, y, 0), Quaternion.identity, rockParent);
                }
                // 풀 
                int randBushCount = Random.Range(3, 5);
                for (int i = 0; i < randBushCount; i++)
                {
                    float x = Random.Range(area.x, area.x + area.width);
                    float y = Random.Range(area.y, area.y + area.height);
                    int randBushIdx = Random.Range(0, bushs.Length);
                    Instantiate(bushs[randBushIdx], new Vector3(x, y, 0), Quaternion.identity, bushParent);
                }
            }
        }
    }
}
