using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaitThrowController : MonoBehaviour
{
    public List<GameObject> baitPrefabs;
    public Transform throwPoint;
    public LineRenderer lineRenderer;

    [SerializeField] private float maxPower;
    [SerializeField] private float throwAngle;
    public float previewLength = 0.2f;
    public int previewResolution = 30;


    float currentPower = 0f;
    bool isIncreasing = true;

    int currentBaitIndex = -1; 
    bool isBaitReady = false; 
    bool readyNextFrame = false;
    public Image powerUI;

    private void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(-0.9f, -0.9f, Camera.main.nearClipPlane + 5f));
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


        if (Input.GetMouseButton(0))
        {

            if (isIncreasing)
            {
                currentPower += Time.deltaTime * maxPower;
                if (currentPower >= maxPower)
                {
                    currentPower = maxPower;
                    isIncreasing = false;
                }
            }
            else
            {
                currentPower -= Time.deltaTime * maxPower;
                if (currentPower <= 0f)
                {
                    currentPower = 0f;
                    isIncreasing = true;
                }
            }

            ShowTrajectory(currentPower);
            UpdatePowerUI(currentPower);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Throw(currentPower);
            HidePreview();
            currentPower = 0f;
            UpdatePowerUI(currentPower); 

            isBaitReady = false;
            currentBaitIndex = -1;
        }
    }

    void Throw(float power)
    {
        if (currentBaitIndex < 0 || currentBaitIndex >= baitPrefabs.Count) return;

        GameObject prefab = baitPrefabs[currentBaitIndex];

        

        GameObject obj = Instantiate(prefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);
    }

    void ShowTrajectory(float power)
    {
        Vector3[] points = new Vector3[previewResolution];
        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        Vector3 startPos = throwPoint.position;
        Vector2 velocity = dir * power;

        for (int i = 0; i < previewResolution; i++)
        {
            float t = i * previewLength / previewResolution;
            Vector2 pos = (Vector2)startPos + velocity * t + 0.5f * Physics2D.gravity * t * t;
            points[i] = pos;
        }

        lineRenderer.positionCount = previewResolution;
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }

    void HidePreview()
    {
        lineRenderer.enabled = false;
    }

    // 버튼에서 호출
    public void SetBaitIndex(int index)
    {
        currentBaitIndex = index;
        readyNextFrame = true;
        Debug.Log("미끼 장전 완료: " + index);
    }

    void UpdatePowerUI(float power)
    {
        if (powerUI != null)
        {
            powerUI.fillAmount = power / maxPower;
        }
    }
}
