using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CookingBoilMiniGame : CookingMiniGameBase
{
    [SerializeField] private Transform scoop;

    // 참조
    //public StartPoint startPoint;
    public GameObject startPointPrefab;
    public GameObject arrowPrefab;
    public DrawLine drawLine;

    //float[] spawnTimes = { };
    //int lineIndex = 0;
    float spawnInterval = 4f;
    float drawTime = 1.5f; // 드로잉 제한시간

    public Vector2 arrowStartPos;
    public Vector2 arrowEndPos;

    public List<Vector2> randomPoints = new();

    float spawnTimer = 0f;
    float spawnMaxTime = 2f; // 이 안에 클릭하지 않으면 사라짐
    bool isClicked = false;

    protected override float GetTimer()
    {
        return data.BoilTimer;
    }

    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }

    public override void StartGame()
    {
        isGameOver = false;
        elapsedTimer = 0f;
        playTime = 0f;
        timer = 15f;

        StartPoint.OnClickStartPoint += SpawnArrowPoint;
    }

    public override void StopGame()
    {
    }

    protected override void UpdateGamePlay()
    {
        // 1. 0초/ 4초/ 8초/ 12초에 스타트 포인트 스폰
        if (elapsedTimer >= spawnInterval)
        {
            elapsedTimer = 0f;
            SpawnStartPoint();
        }

        // 2. 스타트 포인트를 누르면 화살표 활성화
        // 2초안에 누르지 않으면 사라짐
        // -> START POINT에서 해주는 중

        // 3. 화살표 생기면 drawTime동안 선 그릴 수 있음
        // DrawLine.cs
     
        // 4. 그린 선을 판정함수로 넘김
        //Judge()
    }


    public void SpawnStartPoint()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-2f, 2f), 0f);
        var spawnPoint = Instantiate(startPointPrefab, pos, Quaternion.identity);
        Debug.Log(pos);
        spawnPoint.GetComponent<StartPoint>().Init(pos, OnStartPointMissed);
    }

    // 미스 로직
    void OnStartPointMissed()
    {

    }

    void SpawnArrowPoint(Vector3 pos)
    {
        GameObject arrowGo = Instantiate(arrowPrefab, pos, Quaternion.identity);

        var arrow = arrowGo.GetComponent<Arrow>();
        arrow.SpawnRandomArrow((randomPoints) =>
        {
            drawLine.DrawingLine(randomPoints, 1.5f, mousePoints => {
                Destroy(arrowGo);
            });
        }
        );
    }



    // 유저가 그린 선과 화살표를 비교하는 함수
    List<Vector2> Judge(List<Vector2> line)
    {
        List<Vector2> points = new List<Vector2>();

        // 내가 그린 선과 랜덤 선과 일치를 비교
        float distance = Vector3.Distance(arrowStartPos, arrowEndPos);

        return null;
    }






    // 결과
    public void Result()
    {
        
    }


    public void TriggerAnimation()
    {
        scoop.DOLocalMoveY(2f, 0.3f).OnComplete(() => { scoop.DOLocalMoveY(1.55f, 0.3f).OnComplete(() => scoop.DOLocalMoveY(1.79f, 0.5f)); });
    }
}
