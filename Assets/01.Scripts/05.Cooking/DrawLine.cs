using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저가 직접 그리는 선
/// </summary>
public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab; 

    Vector3 drawStartPos;
    Vector3 drawEndPos;
   
    LineRenderer line;
    EdgeCollider2D edgeCollider;

    List<Vector2> mousePoints = new(); // 직접 그리는 선의 점

    private bool canDraw = false;

    private Action<List<Vector2>> onFinish;

    private Coroutine coroutine;

    public void DrawingLine(List<Vector2> arrowLine, float timeLimit, Action<List<Vector2>> callback)
    {
        if (mousePoints.Count > 0) 
        {
        mousePoints.Clear();
        }

        //if (line != null ) { line = null; }
        onFinish = callback;
        canDraw = true;
        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = StartCoroutine(EndDrawTime(timeLimit));
    }

    void OnDestroy()
    {
        StopAllCoroutines();
        onFinish = null;
        line = null;
    }

    private IEnumerator EndDrawTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (this == null) yield break;
        canDraw = false;
        onFinish?.Invoke(mousePoints);
        if (line != null) Destroy(line.gameObject);
    }

    private void Update()
    {
        if (!canDraw) return;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject prefab = Instantiate(linePrefab);
            line = prefab.GetComponent<LineRenderer>();
            edgeCollider = prefab.GetComponent<EdgeCollider2D>();
            Vector3 mousePos = Input.mousePosition;

            //mousePos.z = 10f;
            //mousePoints.Add(Camera.main.ScreenToWorldPoint(mousePos));

            //line.positionCount = 1;
            line.positionCount = 0;

            //line.SetPosition(0, mousePoints[0]);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            
            mousePos.z = 10f;
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
            if (mousePoints.Count == 0 || Vector2.Distance(mousePoints[mousePoints.Count - 1], pos) > 0.1f)
            {
                mousePoints.Add(pos);
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, pos);
                edgeCollider.points = mousePoints.ToArray();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
           // mousePoints.Clear();
           //line.gameObject.SetActive(false);
        }
    }
    public void Judge()
    {

    }
}
