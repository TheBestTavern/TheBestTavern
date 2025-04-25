using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public GameObject startPointPrefab;

    private Vector3 spawnPos;
    float spawnTimer = 0f;
    float spawnMaxTime = 2f; // 이 안에 클릭하지 않으면 사라짐
    bool isClicked = false;
   


    public static event Action<Vector3> OnClickStartPoint;
    private Action OnMiss; // 스타트포인트 미클릭

    private GameObject spawnPoint;

    public void Init(Vector3 pos, Action onMiss)
    {
        spawnPos = pos;
        OnMiss = onMiss;
    }

    //public void SpawnStartPoint()
    //{
    //    spawnPoint = Instantiate(startPointPrefab, randomPos, Quaternion.identity);

    //    StartCoroutine(PointNotClicked());
    //}

    //IEnumerator PointNotClicked()
    //{
    //    yield return new WaitForSeconds(spawnMaxTime);
    //    if (!isClicked)
    //    {
    //        Destroy(spawnPoint);
    //    }
    //}

    private void Update()
    {
        if (isClicked) return;
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnMaxTime)
        {
            OnMiss?.Invoke();
            Destroy(gameObject);
        }
    }

    // '스타트 포인트' 클릭 시 화살표 프리팹 생성해주고, 자신은 삭제
    private void OnMouseDown()
    {
        if (isClicked) return; 
        isClicked = true;
        OnClickStartPoint?.Invoke(spawnPos);
        Destroy(gameObject);
    }

}
