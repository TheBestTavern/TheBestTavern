using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLineController : MonoBehaviour
{
    public Transform lineStart;       // 낚싯줄 시작점
    public Transform lineEndTarget;   // 낚싯줄 끝점
    private LineRenderer lineRenderer;

    public float shakeIntensity = 0.1f;
    public TensionGauge tensionGauge;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (lineEndTarget == null) return;

        Vector3 endPos = lineEndTarget.position;

        // 게이지 값이 0.8 이상일 때만 흔들림 적용
        if (tensionGauge != null && tensionGauge.currentValue >= 0.8f)
        {
            float shakeAmount = Mathf.Sin(Time.time * 40f) * shakeIntensity;
            endPos.x += shakeAmount;
        }

        lineRenderer.SetPosition(0, lineStart.position);
        lineRenderer.SetPosition(1, endPos);
    }
}
