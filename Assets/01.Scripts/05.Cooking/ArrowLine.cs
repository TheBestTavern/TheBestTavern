//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ArrowLine : MonoBehaviour
//{
//    public LineRenderer arrowRenderer;

//    public GameObject arrowHeadPrefab;

//    public Vector3 arrowStartPos;
//    public Vector3 arrowEndPos;

//    public List<Vector2> randomPoints = new();


//    public void SpawnRandomArrow(Action<Vector2, Vector2, List<Vector2>> onReady)
//    {
//        randomPoints.Clear();

//        arrowStartPos = new Vector3(transform.position.x, transform.position.y, 0f);
//        Debug.Log($"arrow : {arrowStartPos}");

//        float angle = UnityEngine.Random.Range(0f, 360f);
//        float length = UnityEngine.Random.Range(2.5f, 3.5f);
//        Vector2 dir = Quaternion.Euler(0,0,angle) * Vector2.right;

//        arrowEndPos = arrowStartPos + (Vector3)dir * length;

        
//        arrowRenderer.positionCount = 2;
//        arrowRenderer.SetPosition(0, arrowStartPos);
//        arrowRenderer.SetPosition(1, arrowEndPos);

//        // 화살표 촉
//        GameObject arrowHead = Instantiate(arrowHeadPrefab);
//        arrowHead.transform.position = arrowEndPos;

//        float headAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//        arrowHead.transform.rotation = Quaternion.Euler(0f, 0f, headAngle);
//        arrowHead.transform.position = new Vector3(arrowHead.transform.position.x, arrowHead.transform.position.y, 0f);


//        //arrowRenderer.widthCurve = new AnimationCurve
//        //(
//        //    new Keyframe(0, 0.4f),
//        //    new Keyframe(0.999f - percent, 0.4f),
//        //    new Keyframe(1 - percent, 1f),
//        //    new Keyframe(1 - percent, 1f),
//        //    new Keyframe(1, 0f));

//        for (int i = 0; i < 10; i++)
//        {
//            randomPoints.Add(Vector2.Lerp(arrowStartPos, arrowEndPos, i / 9f));
//        }

//        onReady?.Invoke(arrowStartPos, arrowEndPos, randomPoints);
//    }
//}
