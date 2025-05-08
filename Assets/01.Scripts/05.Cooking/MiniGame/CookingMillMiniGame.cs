using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingMillMiniGame : CookingMiniGameBase
{
   public MileStoneController mileStoneController;
   [SerializeField] private CookingMillUI millUI;

    // 기준
    public int direction; // 시계 or 반시계
    public bool isClockWise; // 시계방향인지 아닌지
    public float angle; 
    public float speed;

    public float maintainTime; // 목표 속도를 유지한 시간

    Vector2 curPos; // 현재 마우스 위치
    //Vector2 previousPos;
    //Vector2 targetPos;
    //Vector2 centerPos;

    [SerializeField]private RectTransform centerTransform;

    private float curAngle;
    private float previousAngle;

    // 큐 (최단각도, 시간) 구조 
    public Queue<(float deltaAngle, float time)> angleQueue = new();

    float CurAngleFromCenter(Vector2 pos, Vector2 center)
    {
        Vector2 dir = pos - center; // 센터->pos 방향
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    protected override float GetTimer()
    {
        return data.MillTimer;
    }

    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }

   
    public override void StartGame()
    {
        elapsedTimer = 0f;
        playTime = 0f;

        direction = Random.Range(0, 2) == 0 ? 1 : -1; // 시계방향 1, 반시계방향 -1
    }

    public override void StopGame()
    {
        var grade = JudgeGrade();
        CookingMiniGameManager.Instance.SetMiniGameResult(grade);
    }


    /// <summary>
    /// 게임종료 후 등급 판정 
    /// (mouseSpeed를 180f~250f 사이로 유지한 시간을 기준으로 함)
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="time"></param>
    CookingResultGrade JudgeGrade()
    {
        Debug.Log($"{maintainTime}");
        
        if (maintainTime >= data.PerfectTime) 
        {
            Debug.Log("Perfect");
            return CookingResultGrade.Legendary;
        }
        if (maintainTime >= data.GoodTime && maintainTime < data.PerfectTime)
        {
            Debug.Log("Good");
            return CookingResultGrade.Rare;
        }
        if (maintainTime >= data.BadTime && maintainTime < data.GoodTime)
        {
            Debug.Log("Bad");
            return CookingResultGrade.Common;
        }
        else
        {
            Debug.Log("Fail");
            return CookingResultGrade.Failed;
        }

        // 12초이상 : 상 (perfect)
        // 8~11초 : 중 (Good)
        // 5~7초 : 하 (Bad)
        // 5초 미만 || 반대방향 지속 입력 : 실패 (Miss)
    }

    protected override void UpdateGamePlay()
    {
        if(isGameOver) return;

        if (mileStoneController.isDragging)
        {
            curPos = Input.mousePosition; // 현 마우스의 위치
            Vector2 centerPos = RectTransformUtility.WorldToScreenPoint(null, centerTransform.position); // 맷돌 정가운데 (중심점)
       
            // 현재 마우스와 중심 사이의 각도를 구한다
            curAngle = CurAngleFromCenter(curPos, centerPos);

            // 1. 프레임마다의 각도의 차이를 구한다
            float deltaAngle = Mathf.DeltaAngle(curAngle, previousAngle);

            // 2. 마우스 회전방향 
            // (현재 프레임 - 이전 프레임)의 최단각도가 플러스면 시계, 마이너스면 반시계 방향
            //bool curDir = JudgeDirection(deltaAngle);

           // 3. (현재 프레임 - 이전 프레임)의 각도를 계속해서 큐에 넣어준다
            angleQueue.Enqueue((deltaAngle, Time.time));

            // * 0.75초마다 계산하기 위해, 큐에 있는 가장 오래된 데이터의 시간값이 0.75초를 넘으면 제거한다
            if (Time.time - angleQueue.Peek().time > 0.75f)
            {
                angleQueue.Dequeue();
            }

            // 4. 누적된 회전 각도는 큐에 존재하는 데이터들의 절댓값의 합
            float totalAngle = angleQueue.Sum(x => Mathf.Abs(x.deltaAngle));

            // 5. 그것을 0.75초로 나누어주면 마우스의 평균 속도  
            float mouseSpeed = totalAngle / 0.75f;

            // 6. mouseSpeed (180f~250f) 사이 & 올바른 회전방향 유지한 시간도 구해야 판정 기준에 쓸 수 있음
            if (mouseSpeed >= 200f && mouseSpeed <= 400f && JudgeDirection(deltaAngle)) // SO로 옮기기
            {
                maintainTime += Time.deltaTime;
            }

            // 
            millUI.UpdateUI(mouseSpeed, JudgeDirection(deltaAngle));

            // 7.. 모든 처리 이후 초기화
            previousAngle = curAngle;

            Debug.Log($"{mouseSpeed}");
       }
    }

    // 방향 판단
    public bool JudgeDirection(float angle)
    {
        if (direction == 1) // 시계방향회전일 때
        {
            if (angle > 0) isClockWise = true; // 시계방향으로 회전
            else isClockWise = false; // 반시계로 회전
        }
        else // 반시계방향일 때
        {
            if (angle > 0) isClockWise = false; // 시계방향으로 회전
            else isClockWise = true; // 반시계로 회전
        }
        return isClockWise;
    }
}
