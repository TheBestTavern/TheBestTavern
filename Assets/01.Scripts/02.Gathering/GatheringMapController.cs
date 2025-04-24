using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;

public class GatheringMapController : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, .3f);
    [SerializeField] List<Rect> spawnAreas;

    [SerializeField] LayerMask gatheringPropsLayerMask;

    [SerializeField] GameObject[] trees;
    [SerializeField] GameObject[] bushes;
    [SerializeField] GameObject[] fields;

    [SerializeField] private Transform propsParent;

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
        int fieldIdx = Random.Range(0, fields.Length);
        foreach (var area in spawnAreas)
        {
            int randProps = Random.Range(0, 4);
            // 밭
            if (randProps == 0)
            {
                float x = area.x + area.width / 2;
                float y = area.y + area.height / 2;
                Instantiate(fields[fieldIdx], new Vector3(x, y, 0), fields[fieldIdx].transform.rotation, propsParent);
            }
            else
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
                int randBushCount = Random.Range(0, 6);
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
    }

    void SetSortingLayer(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = -(int)(obj.transform.position.y * 100);
        }

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = -(int)(obj.transform.position.y * 100) + i + 1;
        }
    }
}
