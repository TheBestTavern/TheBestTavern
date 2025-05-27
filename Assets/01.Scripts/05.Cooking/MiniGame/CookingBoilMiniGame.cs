using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using Bitgem.VFX.StylisedWater;


public class CookingBoilMiniGame : CookingMiniGameBase
{
    [SerializeField] private Transform scoop;
    [SerializeField] private WaterVolumeTransforms water;

    // 참조
  
    public GameObject startPointPrefab;
    public GameObject arrowPrefab;
    public DrawLine drawLine;

    public Vector2 arrowStartPos;
    public Vector2 arrowEndPos;

    public List<Vector2> randomPoints = new();

   // float spawnTimer = 0f;
    //float spawnMaxTime = 2f; // 이 안에 클릭하지 않으면 사라짐
    //bool isClicked = false;

    [SerializeField] private Transform parent;

    private int perfect, bad, good, miss;

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
        SoundManager.Instance.PlayAmbience("BoilAmbience");

        StartPoint.OnClickStartPoint -= SpawnArrowPoint;
        StartPoint.OnClickStartPoint += SpawnArrowPoint; 
        drawLine = FindObjectOfType<DrawLine>();
    }

    private void OnDestroy()
    {
        StartPoint.OnClickStartPoint -= SpawnArrowPoint;
    }

    protected override void UpdateGamePlay()
    {
        // 1. 0초/ 4초/ 8초/ 12초에 스타트 포인트 스폰
        if (elapsedTimer >= data.SpawnInterval)
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
        //Judge();
    }


    public void SpawnStartPoint()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-2f, 2f), 0f);
        var spawnPoint = Instantiate(startPointPrefab, pos, Quaternion.identity, parent);
        Debug.Log(pos);
        spawnPoint.GetComponent<StartPoint>().Init(pos, OnStartPointMissed);
    }

    // 미스 로직
    void OnStartPointMissed()
    {
        miss++;
    }

    void SpawnArrowPoint(Vector3 pos)
    {
        if (drawLine == null || drawLine.Equals(null))
        {
            drawLine = FindObjectOfType<DrawLine>();

            if (drawLine == null || drawLine.Equals(null))
            {
                Debug.LogError("❌ drawLine is null or missing (파괴된 참조 포함)");
                return;
            }
        }

        GameObject arrowGo = Instantiate(arrowPrefab, pos, Quaternion.identity, parent);

        var arrow = arrowGo.GetComponent<Arrow>();
        arrow.SpawnRandomArrow((randomPoints) =>
        {
            drawLine.DrawingLine(randomPoints, 1.5f, mousePoints => {
                Destroy(arrowGo);

                Judge(mousePoints, randomPoints);
                Debug.Log($"유저라인{mousePoints.Count}");
            });
        }
        );
    }

    float GetLineLength(List<Vector2> points)
    {
        if (points.Count <= 2) return 0f;

        float length = 0f;

        for (int i = 1; i < points.Count; i++)
        {
            length += Vector2.Distance(points[i - 1], points[i]);
        }

        return length;
    }

    float GetOverlapRatio(List<Vector2> userLine, List<Vector2> targetLine)
    {
        if (userLine.Count <= 2 || targetLine.Count <= 2) return 0f;

        int matchCount = 0;

        foreach (var userPoint in userLine)
        {
            foreach (var targetPoint in targetLine)
            {
                if (Vector2.Distance(userPoint, targetPoint) < 0.18f)
                {
                    matchCount++;
                    break;
                }
            }
        }
        if (matchCount == 0) return 0f;
        return (float)matchCount / userLine.Count;
    }

    float GetDirection(List<Vector2> userLine, List<Vector2> targetLine)
    {
        if (userLine.Count <= 2 || targetLine.Count <= 2) return 0f;

        Vector2 userDir = (userLine[^1] - userLine[0]).normalized;
        Vector2 targetDir = (targetLine[^1] - targetLine[0]).normalized;

        float dot = Vector2.Dot(userDir, targetDir); // -1 ~ 1
        return (dot + 1f) / 2f; // 0~1 스코어로 변환
    }

    // 유저가 그린 선과 화살표를 비교하는 함수
    void Judge(List<Vector2> userLine, List<Vector2> targetLine)
    {
        List<Vector2> points = new();

        float userLength = GetLineLength(userLine);
        float targetLength = GetLineLength(targetLine);
        
        float lengthRatio = userLength / targetLength;
        float lengthScore = Mathf.Clamp01(1f - Mathf.Abs(1f - lengthRatio));

        float overlapRatio = GetOverlapRatio(userLine, targetLine);

        float directionScore = GetDirection(userLine, targetLine);

        // 내가 그린 선과 랜덤 선과 일치를 비교
        //float distance = Vector3.Distance(arrowStartPos, arrowEndPos);

        float finalScore = (overlapRatio * 0.5f) + (lengthScore * 0.25f) + (directionScore * 0.25f);
        // 방향 + 각도 완벽히 일치 + 선 근접도 90% 이상
        if (finalScore >= 0.9f)
        {
            perfect++;
            TriggerAnimation();
            TriggerWaterWave();
            SoundManager.Instance.PlaySFX("Boil");
            CookingEffectManager.Instance.ShowJudgeText(0);
        }
        // 약간 어긋났지만 전체 선이동 70% 이상
        else if (finalScore >= 0.7f)
        {
            good++;
            TriggerAnimation();
            TriggerWaterWave();
            SoundManager.Instance.PlaySFX("Boil");
            CookingEffectManager.Instance.ShowJudgeText(1);
        }
        // 방향불일치 or 선이탈 40~70%
        else if (finalScore >= 0.4f)
        {
            bad++;
            CookingEffectManager.Instance.ShowJudgeText(2);
        }
        // 클릭안함/엉뚱한방향/40%미만
        else if (finalScore < 0.4f || userLine == null || userLine.Count <= 3)
        {
            miss++;
            CookingEffectManager.Instance.ShowJudgeText(3);
        }

        Debug.Log($"점수 :{ finalScore} 미스 횟수 : {miss}");
    }

    public override void StopGame()
    {
        var grade = JudgeGrade();
        Debug.Log($"재료최종등급:{grade}");
        CookingMiniGameManager.Instance.SetMiniGameResult(grade);
        RecipeManager.Instance.EndCooking();

    }

    // 결과
    CookingResultGrade JudgeGrade()
    {
        // miss 3회이상
        if (miss >= data.MissBoilCount)
            return CookingResultGrade.Failed;
        //퍼펙트4회
        else if (perfect >= data.PerfectBoilCount)
            return CookingResultGrade.Legendary;

        // good 3회 이상
        else if (good >= data.GoodBoilCount)
            return CookingResultGrade.Rare;

        // bad/miss 2회이상
        else if (bad >= data.BadBoilCount || miss >= data.BadBoilCount)
            return CookingResultGrade.Common;

        return CookingResultGrade.Failed;
    }

    public void TriggerAnimation()
    {
        scoop.DOLocalMoveY(0f, 0.3f).OnComplete(() => { scoop.DOLocalMoveY(-1f, 0.3f).OnComplete(() => scoop.DOLocalMoveY(-0.29f, 0.5f)); });
    }

    public void TriggerWaterWave()
    {
        water = water.GetComponent<WaterVolumeTransforms>();
        var waterRenderer = water.GetComponent<Renderer>();
        Material mat = waterRenderer.material;
        float originValue = mat.GetFloat("_WaveScale");

        Sequence seq = DOTween.Sequence();
        seq.Append(mat.DOFloat(0.1f, "_WaveScale", 1f));
        seq.AppendInterval(2f);
        seq.Append(mat.DOFloat(originValue, "_WaveScale", 1f));

        //waterRenderer.material.SetFloat("_WaveScale", 0.1f);
    }
}
