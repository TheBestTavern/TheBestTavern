using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 도마게임 로직 구현부
/// : 타이밍 퍼즐 (리듬 입력형)
/// ** 구현하다 중단되었습니다!!
/// </summary>
public class CookingCuttingMiniGame : CookingMiniGameBase
{
    
    [SerializeField][field:Range(0f, 5f)] private float barSpeed; // *타이밍 바 (게이지) 이동 속도는 재료마다 다르게 설정 가능*

    private float[] generateBarTiming = { 0f, 5f, 10f };  // 타이밍바가 생성되는 시간 (0초, 5초, 10초)
    private int currentCut; // 현재 썰기 타이밍이 몇번째인지

    [SerializeField] private float endTime; // 타이밍바 제거
    [SerializeField] private float JudgeCheckTime; // 판정시간 (오차 범위 체크용)

    public GameObject knifePrefab; // 칼 프리팹
    public GameObject cuttingUIPrefab; // UI 프리팹

    public Button clickButton; // 썰기 버튼

    public GameObject judgeLinePrefab; // 판정선 프리팹
    public GameObject barPrefab; // 타이밍바 프리팹
    public GameObject myBar; // 플레이어가 클릭했을 때 타이밍바의 위치

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


        // 첫 타이밍 바 프리팹 생성 및 초기화
        SpawnTimingBar();

        // 첫 칼날 프리팹 생성 및 초기화 
        SpawnKnife();

        currentCut = 0;

        // UI 생성 및 활성화
        //cuttingUIPrefab = Resources.Load<GameObject>("Prefabs");
    }


    public void SpawnTimingBar()
    {
        // 5초 간격으로 칼날 궤도에 표시점 (타이밍바)이 에셋의 길이 왼쪽에서 생성 3번 
    }

    private void SpawnJudgeLine()
    {
        // 5초마다 썰기 지점이 3d 에셋의 길이를 기준으로 25% , 50%, 75%에서 생성되도록
    }

    private void SpawnKnife()
    {
        // 썰기 애니메이션이 나올때 프리팹 생성
    }


    // 오차범위를 통한 클릭 판정
    // 판정 : Perfect(±0.2) / Good (±0.4) / Bad (±0.6) / Miss (오차 초과/미입력)
    public void TimingJudge(float diff)
    {
        if (diff <= 0.2f) { Debug.Log("Perfect"); }
        else if (diff <= 0.4f) { Debug.Log("Good"); }
        else if (diff <= 0.6f) { Debug.Log("Bad"); }
        else { Debug.Log("Miss"); }
    }

    public bool OnButtnClicked()
    {
        return true;
    }

    public override void StopGame()
    {
        
    }

    //public bool IsGameOver() 
    //{
    //    if (timer == 0f) return true;
    //    // 게임 경과 15초에 게임 종료
    //}

    public void SetGrade()
    {
        // 최종 등급 결정 - 상, 중, 하, 실패
        // 획득한 재료 결정 - 상이면 최고급 재료, 중이면 일반 재료, 하면 하품질재료, 실패 시 가공 실패 및 재료 손상
    }

    protected override void UpdateGamePlay()
    {
        // 마우스를 따라서 칼날 프리팹이 천천히 위아래로 움직이면서 썰기 타이밍을 연출


        // 타이밍바 스폰 시간이 됐을 때 && 3번까지만 스폰  
        if (elapsedTimer >= generateBarTiming[currentCut] && currentCut < generateBarTiming.Length)
        {
            SpawnTimingBar();

            SpawnJudgeLine();

            currentCut++;
        }

        // 타이밍바가 생성되면, barSpeed에 따라 좌우로 왕복


        // 타이밍 바가 썰기 지점(선)에 왔을 때 버튼 (전체화면 사이즈) 클릭했으면
        if (OnButtnClicked())
        {
            float diff = Mathf.Abs(judgeLinePrefab.transform.position.x - myBar.transform.position.x);
            TimingJudge(diff);

            // 썰기 애니메이션 출력

            // 타이밍바가 그 지점에서 멈춤

            // 판정에 따른 이펙트 발생

            // 타이밍 바 비활성화 or 삭제
        }

        // 타이밍 바가 생성된 직후 3초동안 버튼을 클릭하지 않았을 때 자동으로 Miss 판정되고, 타이밍바 비활성화 or 삭제

    }
}
