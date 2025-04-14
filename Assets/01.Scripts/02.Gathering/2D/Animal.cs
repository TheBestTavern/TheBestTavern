using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalSizeType
{
    Small,
    Medium,
    Large,
}

public class Animal : MonoBehaviour
{
    public string animalName;
    public AnimalSizeType sizeType;
    public string[] favoriteBaits;

    private float baseCaptureChance = 0.2f;
    private float captureChance = 0f;
    private bool canBeCaptured = false;

    private Transform targetBait;
    private float stayNearBaitTimer = 0f;
    private float requiredStayTime = 3f;
    private float stayDistance = 2f;

    private Coroutine checkProximityCoroutine;

    private bool isStunned = false;
    private float stunDuration = 3f; // 기절 시간 (초)
    private Coroutine stunCoroutine;

    public void ReactToBait(string baitType, Vector3 baitPosition)
    {
        bool likesBait = System.Array.Exists(favoriteBaits, bait => bait == baitType);

        switch (sizeType)
        {
            case AnimalSizeType.Small:
                Debug.Log($"{gameObject.name} (Small)이(가) {baitType} 미끼 없이 즉시 반응하여 캡처 가능합니다! 돌을 던지세요.");
                SetCaptureChanceInstant(baitType, baitPosition, true);
                break;

            case AnimalSizeType.Medium:
                if (likesBait)
                {
                    SetCaptureChanceInstant(baitType, baitPosition, true);
                    Debug.Log($"{gameObject.name} (Medium)이(가) 좋아하는 {baitType} 미끼를 발견하고 이동 중! 이제 돌을 던져서 캡처하세요.");
                    if (checkProximityCoroutine != null) StopCoroutine(checkProximityCoroutine);

                    GameObject foundBait = GameObject.Find(baitType);
                    if (foundBait != null)
                        targetBait = foundBait.transform;

                    checkProximityCoroutine = StartCoroutine(CheckProximityAndIncreaseChance(baitType));
                }
                else
                {
                    Debug.Log($"{gameObject.name} (Medium)은 {baitType} 미끼를 좋아하지 않아서 반응하지 않음.");
                }
                break;

            case AnimalSizeType.Large:
                if (likesBait)
                {
                    Debug.Log($"{gameObject.name} (Large)은 {baitType} 미끼를 싫어해서 3초 뒤 도망감! 이제 미끼를 던지고 도망가세요.");
                    // After 3 seconds, the large animal will flee. You must throw bait and run away
                    Invoke(nameof(Flee), 3f); // Make the animal flee after 3 seconds
                }
                else
                {
                    Debug.Log($"{gameObject.name} (Large)은 {baitType} 미끼를 무시하고 도망가지 않음.");
                    // The large animal doesn't react to the bait and does not flee
                }
                break;
        }
    }

    void SetCaptureChanceInstant(string baitType, Vector3 baitPosition, bool isBaitEffective)
    {
        if (!isBaitEffective)
        {
            captureChance = 0f;
            canBeCaptured = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, baitPosition);
        float bonus = Mathf.Clamp01(1f - distance / 5f);
        float bonusChance = bonus * 0.5f;

        captureChance = baseCaptureChance + bonusChance;
        canBeCaptured = true;

        Debug.Log($"[동물: {animalName}] 미끼({baitType}) 반응. 거리: {distance:F2}, 확률: {captureChance * 100:F1}%");
    }

    IEnumerator CheckProximityAndIncreaseChance(string baitType)
    {
        stayNearBaitTimer = 0f;
        canBeCaptured = false;

        while (true)
        {
            if (targetBait == null)
                yield break;

            float distance = Vector3.Distance(transform.position, targetBait.position);

            if (distance <= stayDistance)
            {
                stayNearBaitTimer += Time.deltaTime;

                if (stayNearBaitTimer >= requiredStayTime)
                {
                    float timeFactor = Mathf.Clamp01(stayNearBaitTimer / 5f);
                    float bonusChance = timeFactor * 0.6f; // 최대 +60%
                    captureChance = baseCaptureChance + bonusChance;
                    canBeCaptured = true;

                    Debug.Log($"[동물: {animalName}] 미끼 근처에 머무름: {stayNearBaitTimer:F1}s → 확률: {captureChance * 100:F1}%");
                }
            }
            else
            {
                stayNearBaitTimer = 0f;
                captureChance = baseCaptureChance;
                canBeCaptured = false;
            }

            yield return null;
        }
    }
    

    void Flee()
    {
        Debug.Log($"{animalName}이(가) 도망쳤습니다!");
        Destroy(gameObject);
    }

    public bool CanBeCaptured() => canBeCaptured;

    public bool TryCapture()
    {
        if (!canBeCaptured)
        {
            Debug.Log($"{animalName}은(는) 아직 포획할 수 없습니다.");
            return false;
        }

        float roll = Random.value;
        bool success = roll < captureChance;

        Debug.Log($"[동물: {animalName}] 포획 시도! 확률: {captureChance * 100:F1}% → 랜덤값: {roll:F2} → {(success ? "성공" : "실패")}");

        if (success)
        {
            Debug.Log($"{animalName} 포획 성공");
            Destroy(gameObject);
        }
        return success;
    }

    public void GetHitByRock()
    {
        if (isStunned)
            return;

        Debug.Log($"{animalName}이(가) 돌에 맞아 기절했습니다!");
        isStunned = true;

        // 여기에서 애니메이션 멈추거나 이동을 멈추는 처리를 추가하면 좋아요
        // 예: animator.enabled = false; or agent.isStopped = true;

        if (stunCoroutine != null)
            StopCoroutine(stunCoroutine);

        stunCoroutine = StartCoroutine(StunRoutine());
    }

    private IEnumerator StunRoutine()
    {
        yield return new WaitForSeconds(stunDuration);

        Debug.Log($"{animalName}이(가) 기절에서 깨어났습니다.");
        isStunned = false;

        // 애니메이션이나 이동 재개
        // 예: animator.enabled = true; or agent.isStopped = false;
    }
}
