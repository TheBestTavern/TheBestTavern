using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public GameObject throwObjectPrefab;
    public Transform throwPoint;
    public LineRenderer lineRenderer;

    public float maxPower = 10f;
    public float throwAngle = 120f; // 👈 고정된 각도
    public float previewLength = 1f; // 선의 곡선 시뮬레이션 길이
    public int previewResolution = 30; // 선의 포인트 수

    float holdTime;
    float maxHoldTime = 2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);

            float power = (holdTime / maxHoldTime) * maxPower;

            ShowTrajectory(power); // 미리보기
        }

        if (Input.GetMouseButtonUp(0))
        {
            float power = (holdTime / maxHoldTime) * maxPower;

            Throw(power);
            HidePreview();
        }
    }

    void Throw(float power)
    {
        GameObject obj = Instantiate(throwObjectPrefab, throwPoint.position, Quaternion.identity);
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
}
