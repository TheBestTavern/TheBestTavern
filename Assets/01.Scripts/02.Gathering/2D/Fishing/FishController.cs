using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [Header("물고기 설정")]
    [SerializeField] private float moveSpeed = 3f; // 물고기의 기본 이동 속도
    [SerializeField] private int[] gatheringKeys;
    [SerializeField] private float resistanceSpeed;
    public float resistanceChance = 0.3f; // 저항 확률 (0.3 = 30%)
    private float resistanceCooldown = 1f; // 저항 시도 간격
    private float lastResistanceTime = 0f;

    private Vector3 direction; // 물고기 방향 (랜덤)
    private Vector3 startPosition; // 초기 위치

    private void Start()
    {
        startPosition = transform.position;
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    void Update()
    {
        if (FishingManager.Instance.fishingStart)
        {
            StartFishing();
        }
        // 물고기의 y값에 따라 크기를 조정
        AdjustFishSize();
    }

    // PullToward: 낚시대를 끌어당기는 기능
    public void PullToward(Vector3 catchZonePosition)
    {
        float step = moveSpeed * Time.deltaTime;

        // 일정 시간마다 저항을 시도
        if (Time.time - lastResistanceTime > resistanceCooldown)
        {
            lastResistanceTime = Time.time;
            if (Random.value < resistanceChance) // 확률적으로 저항
            {
                Vector3 awayFromZone = (transform.position - catchZonePosition).normalized;
                transform.Translate(awayFromZone * resistanceSpeed * Time.deltaTime);
                return; 
            }
        }
        Vector3 target = Vector3.MoveTowards(transform.position, catchZonePosition, step);
        transform.position = target;
    }

    public bool IsCaught(Vector3 catchZonePosition)
    {
        if (Vector3.Distance(transform.position, catchZonePosition) < 1f)
        {
            AddItemtoInventory();
            return true;
        }
        return false;
    }

    public void StartFishing()
    {
        // 물고기가 랜덤으로 화면을 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // 화면 끝을 넘지 않도록 처리 (물고기가 화면 밖으로 나가지 않도록)
        if (transform.position.x < -5.5f || transform.position.x > 5.5f)
        {
            direction.x *= -1;
        }
        if (transform.position.y < -2f || transform.position.y > 3f)
        {
            direction.y *= -1;
        }
    }

    public int GetRandomGatheringKey()
    {
        if (gatheringKeys != null && gatheringKeys.Length > 0)
        {
            int randomIndex = Random.Range(0, gatheringKeys.Length);
            return gatheringKeys[randomIndex];
        }
        else
        {
            Debug.LogWarning("gatheringKeys가 비어있습니다.");
            return -1;
        }
    }

    private void AddItemtoInventory()
    {
        if (InventoryManager.Instance.Invens[InvenType.Gathering].아이템획득(Data.GetRawItem(GetRandomGatheringKey()), 1))
        {
            Debug.Log("아이템 증가");
        }
        else
        {
            Debug.Log("아이템 증가 불가능");
        }
    }

    private void AdjustFishSize()
    {
        float scale = Mathf.Lerp(1f, 0.5f, Mathf.InverseLerp(-5f, 5f, transform.position.y)); // y값에 따른 크기 변화
        transform.localScale = new Vector3(scale, scale, 1f); // x, y 방향으로 크기 조정
    }
}
