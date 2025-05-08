using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;


public abstract class GatheringMapController : MonoBehaviour
{
    [SerializeField] protected Color gizmoColor = new Color(1, 0, 0, .3f);
    [SerializeField] protected List<Rect> spawnAreas;

    [SerializeField] protected Transform propsParent;

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


    protected void SetSortingLayer(GameObject obj)
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
    public abstract void CreateMapProps();
}
