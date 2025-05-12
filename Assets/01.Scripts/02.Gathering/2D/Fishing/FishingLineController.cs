using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLineController : MonoBehaviour
{
    public Transform lineStart;       // 낚싯줄 시작점
    public Transform lineEndTarget;   // 낚싯줄 끝점
    private LineRenderer lineRenderer;

    public float shakeIntensity = 0.5f;
    public TensionGaugeController tensionGauge;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (lineEndTarget == null) return;

        Vector3 endPosition = GetEndPositionWithShake();

        lineRenderer.SetPosition(0, lineStart.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private Vector3 GetEndPositionWithShake() //LineEndTarget의 위치를 끝점으로 설정 후 흔들림 적용 조건 확인
    {
        Vector3 endPos = lineEndTarget.position;

        if (ShouldApplyShake())
        {
            float shake = CalculateShakeOffset();
            endPos.x += shake;
        }

        return endPos;
    }

    private bool ShouldApplyShake() //값이 0.8 이상이면 true 반환
    {
        return tensionGauge != null && tensionGauge.currentValue >= 0.8f;
    }

    private float CalculateShakeOffset() //시간 기반으로 진동값 계산 후 값 반환
    {
        return Mathf.Sin(Time.time * 40f) * shakeIntensity;
    }
}
