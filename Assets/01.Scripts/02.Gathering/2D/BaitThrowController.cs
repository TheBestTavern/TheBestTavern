using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaitThrowController : MonoBehaviour
{
    [Header("프리팹 및 위치 설정")]
    [SerializeField] private GameObject baitObjectPrefab; // Generic bait prefab
    [SerializeField] private Transform throwPoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Camera miniGameCamera;
    [SerializeField] private Image powerUI;
    [SerializeField] private BaitDropArea baitDropArea;

    [Header("파워 및 각도 설정")]
    [SerializeField] private float maxPower;
    [SerializeField] private float throwAngle;
    [SerializeField] private float previewLength = 0.2f;
    [SerializeField] private int previewResolution = 30;

    private float currentPower = 0f;
    private bool isIncreasing = false;
    private bool isBaitReady = false; 
    private bool readyNextFrame = false;
    private ItemStack currentBait;

    private void Start()
    {
        Vector3 bottomLeft = miniGameCamera.ViewportToWorldPoint(new Vector3(-0.9f, -0.9f, miniGameCamera.nearClipPlane + 5f));
        bottomLeft.z = 0f;
        throwPoint.position = bottomLeft;
    }

    void Update()
    {
        if (readyNextFrame)
        {
            isBaitReady = true;
            readyNextFrame = false;
            return;
        }

        if (!isBaitReady) return;

        if (Input.GetMouseButton(1))
        {
            if (isIncreasing)
            {
                IncreasePower();
            }
            else
            {
                DecreasePower();
            }
            ShowTrajectory(currentPower);
            UpdatePowerUI(currentPower);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Throw(currentPower);
            HidePreview();
            currentPower = 0f;
            UpdatePowerUI(currentPower); 
            isBaitReady = false;
            baitDropArea.ClearBait();
        }
    }

    void Throw(float power)
    {
        if (baitObjectPrefab == null || currentBait == null) return;

        GameObject obj = Instantiate(baitObjectPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
        Bait bait = obj.GetComponent<Bait>();

        if (sr != null && currentBait != null)
        {
            sr.sprite = baitDropArea.itemSprite;
        }

        bait.SetBaitKey(currentBait.Origin.key);

        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);

        var inven = InventoryManager.Instance.Invens[InvenType.Gathering];
        inven.LooseItem(currentBait.Origin, 1);
        SoundManager.Instance.PlaySFX("ThrowBaitStrong");
    }

    void ShowTrajectory(float power) //던지기 궤적 함수
    {
        Vector3[] points = new Vector3[previewResolution]; //궤적 구성을 위한 포인트 배열
        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        Vector3 startPos = throwPoint.position;
        Vector2 velocity = dir * power;

        for (int i = 0; i < previewResolution; i++)
        {
            float t = i * previewLength / previewResolution;
            Vector2 pos = (Vector2)startPos + velocity * t + 0.5f * Physics2D.gravity * t * t; //등가속도 운동 공식 사용
            points[i] = pos;
        }
        lineRenderer.positionCount = previewResolution;
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
        //LineRenderer를 통해 실제 게임 화면에서 궤적 그리기
    }

    void HidePreview()
    {
        lineRenderer.enabled = false;
    }

    public void SetBaitIndex(ItemStack bait)
    {
        currentBait = bait;
        readyNextFrame = true;
        Debug.Log("미끼 설정: " + currentBait.Origin.englishName);
    }

    void UpdatePowerUI(float power)
    {
        if (powerUI != null)
        {
            powerUI.fillAmount = power / maxPower;
        }
    }

    private void IncreasePower()
    {
        currentPower += Time.deltaTime * maxPower;
        if (currentPower >= maxPower)
        {
            currentPower = maxPower;
            isIncreasing = false;
        }
    }

    private void DecreasePower()
    {
        currentPower -= Time.deltaTime * maxPower;
        if (currentPower <= 0f)
        {
            currentPower = 0f;
            isIncreasing = true;
        }
    }


}
