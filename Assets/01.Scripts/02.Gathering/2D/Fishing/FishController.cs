using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float moveSpeed = 3f; // 물고기의 기본 이동 속도
    public float resistanceSpeed = 2f; // 저항 속도 (Space를 눌렀을 때 물고기가 저항함)
    public Vector3 direction; // 물고기 방향 (랜덤)

    private Vector3 startPosition; // 초기 위치
    private Vector3 targetPosition; // 목표 위치 (CatchZone)

    private void Start()
    {
        startPosition = transform.position;
        // 물고기의 방향을 랜덤하게 설정
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    void Update()
    {
        // 물고기가 랜덤으로 화면을 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // 화면 끝을 넘지 않도록 처리 (물고기가 화면 밖으로 나가지 않도록)
        if (transform.position.x < -5.5f || transform.position.x > 5.5f)
        {
            direction.x *= -1;
        }
        if (transform.position.y < -3.4f || transform.position.y > 5f)
        {
            direction.y *= -1;
        }

        // 물고기의 y값에 따라 크기를 조정
        AdjustFishSize();
    }

    // PullToward: 낚시대를 끌어당기는 기능
    public void PullToward(Vector3 catchZonePosition)
    {
        // CatchZone 위치로 물고기를 끌어당김
        // 저항이 있을 경우 천천히 이동하도록 조정
        float step = moveSpeed * Time.deltaTime; // 이동 속도

        // 물고기가 CatchZone 위치에 가까워질수록 속도가 감소하게 할 수 있음
        Vector3 target = Vector3.MoveTowards(transform.position, catchZonePosition, step);
        transform.position = target;
    }

    // IsCaught: 물고기가 CatchZone에 도달했는지 확인
    public bool IsCaught(Vector3 catchZonePosition)
    {
        // 물고기와 CatchZone의 거리 계산 후 가까워지면 성공
        if (Vector3.Distance(transform.position, catchZonePosition) < 1f)
        {
            return true;
        }
        return false;
    }

    // 물고기의 크기를 y값에 따라 조정
    private void AdjustFishSize()
    {
        // y값이 클수록 작아지고, 작을수록 커지게 설정
        float scale = Mathf.Lerp(1f, 0.5f, Mathf.InverseLerp(-5f, 5f, transform.position.y)); // y값에 따른 크기 변화
        transform.localScale = new Vector3(scale, scale, 1f); // x, y 방향으로 크기 조정
    }
}
