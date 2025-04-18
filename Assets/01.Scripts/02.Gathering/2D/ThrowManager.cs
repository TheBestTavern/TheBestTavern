using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public List<GameObject> baitPrefabs;
    public Transform throwPoint;
    public LineRenderer lineRenderer;

    public float maxPower = 25f;
    public float throwAngle = 60f;
    public float previewLength = 0.2f;
    public int previewResolution = 30;

    float holdTime;
    float maxHoldTime = 2f;

    int currentBaitIndex = -1; 
    bool isBaitReady = false; 
    bool readyNextFrame = false;

    private void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane + 5f));
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

        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);

            float power = (holdTime / maxHoldTime) * maxPower;
            ShowTrajectory(power);
        }

        if (Input.GetMouseButtonUp(0))
        {
            float power = (holdTime / maxHoldTime) * maxPower;
            Throw(power);
            HidePreview();

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
}
