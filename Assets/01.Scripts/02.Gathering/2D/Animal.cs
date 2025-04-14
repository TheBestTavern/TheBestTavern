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
    public AnimalSizeType animalSizeType;
    public string[] favoriteBaits;
    public bool IsStunned { get; private set; }
    public bool IsHurt { get; private set; }
    public bool BaitEffectApplied { get; private set; }

    private float baseCaptureChance = 0.2f;
    private float captureChance = 0f;
    private bool canBeCaptured = false;

    private Transform targetBait;
    private float stayNearBaitTimer = 0f;
    private float requiredStayTime = 3f;
    private float stayDistance = 2f;

    private Coroutine checkProximityCoroutine;

    private bool isStunned = false;
    private float stunDuration = 3f;
    private Coroutine stunCoroutine;

    private bool isFleeing = false;

    public void ReactToBait(string baitType, Vector3 baitPosition)
    {
        bool likesBait = System.Array.Exists(favoriteBaits, bait => bait == baitType);

        switch (animalSizeType)
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
                    Invoke(nameof(Flee), 3f);
                }
                else
                {
                    Debug.Log($"{gameObject.name} (Large)은 {baitType} 미끼를 무시하고 도망가지 않음.");
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
                    float bonusChance = timeFactor * 0.6f;
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

    public void OnHitByRock()
    {
        if (isFleeing)
        {
            Debug.Log($"{animalName}은 이미 도망 중이므로 돌에 반응하지 않음.");
            return;
        }

        switch (animalSizeType)
        {
            case AnimalSizeType.Small:
                Debug.Log($"{animalName} (Small)이(가) 돌에 맞고 기절했습니다!");
                isStunned = true;
                IsStunned = true; // Update the public property as well
                if (stunCoroutine != null) StopCoroutine(stunCoroutine);
                stunCoroutine = StartCoroutine(StunAndEnableCapture());
                break;

            case AnimalSizeType.Medium:
                if (canBeCaptured)
                {
                    Debug.Log($"{animalName} (Medium)이(가) 돌에 맞고 캡처 가능합니다!");
                    isStunned = true;
                    IsStunned = true; // Update the public property as well
                                      // 돌에 맞아서 포획 가능하게
                }
                else
                {
                    Debug.Log($"{animalName} (Medium)은 아직 미끼 반응 상태가 아님.");
                }
                break;

            case AnimalSizeType.Large:
                Debug.Log($"{animalName} (Large)은 돌에 맞았지만 반응 없음.");
                break;
        }
    }

    IEnumerator StunAndEnableCapture()
    {
        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
        IsStunned = false; // Ensure that the public property reflects the stun change as well
        canBeCaptured = false;

        Debug.Log($"{animalName}의 기절 상태가 종료되었습니다.");
    }

    public bool TryCapture()
    {
        Debug.Log($"{animalName} IsStunned: {IsStunned}"); // Debug log
        if (!CanBeCaptured()) return false;

        // 포획 성공 처리
        Destroy(gameObject);
        return true;
    }

    public void Flee()
    {
        isFleeing = true;
        Debug.Log($"{animalName}이(가) 도망쳤습니다!");
        // 여기에 애니메이션, 이동, 제거 등 처리 가능
        Destroy(gameObject, 1f);
    }

    public void ForceFleeByButton()
    {
        if (animalSizeType == AnimalSizeType.Large && !isFleeing)
        {
            Debug.Log($"{animalName}이(가) 미끼 반응 후 플레이어가 도망을 선택하여 도망칩니다.");
            Flee();
        }
    }

    public bool CanBeCaptured()
    {
        switch (animalSizeType)
        {
            case AnimalSizeType.Small:
                return IsStunned;
            case AnimalSizeType.Medium:
                return IsHurt && BaitEffectApplied;
            case AnimalSizeType.Large:
                return false;
        }
        return false;
    }

    
}
