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

    private float stunDuration = 3f;
    private Coroutine stunCoroutine;

    private bool hasReactedToBait = false;
    private bool isFleeing = false;

    public void ReactToBait(string baitType, Vector3 baitPosition)
    {
        if (hasReactedToBait) return; 
        hasReactedToBait = true;

        bool likesBait = System.Array.Exists(favoriteBaits, bait => bait == baitType);

        switch (animalSizeType)
        {
            case AnimalSizeType.Small:
                Debug.Log($"{animalName} (Small)이(가) {baitType} 미끼 없이도 포획 가능!");
                SetCaptureChanceInstant(baitPosition, true);
                break;

            case AnimalSizeType.Medium:
                if (likesBait)
                {
                    Debug.Log($"{animalName} (Medium)이(가) 좋아하는 {baitType} 미끼 발견! 가까이 오게 함.");
                    BaitEffectApplied = true;

                    if (checkProximityCoroutine != null)
                        StopCoroutine(checkProximityCoroutine);

                    GameObject foundBait = GameObject.Find(baitType);
                    if (foundBait != null)
                        targetBait = foundBait.transform;

                    checkProximityCoroutine = StartCoroutine(CheckProximityAndIncreaseChance());
                }
                else
                {
                    Debug.Log($"{animalName} (Medium)은 {baitType} 미끼를 좋아하지 않아서 반응하지 않음.");
                }
                break;

            case AnimalSizeType.Large:
                if (likesBait)
                {
                    Debug.Log($"{animalName} (Large)은 {baitType}을 싫어해서 도망갑니다!");
                    Invoke(nameof(Flee), 3f); 
                }
                else
                {
                    Debug.Log($"{animalName} (Large)은 {baitType} 미끼를 무시합니다.");
                }
                break;
        }
    }

    // 돌에 맞았을 때 호출
    public void GetHitByRock(Vector3 hitPosition)
    {
        if (animalSizeType == AnimalSizeType.Small)
        {
            SetCaptureChanceInstant(hitPosition, true);
        }
        else if (animalSizeType == AnimalSizeType.Medium)
        {
            if (BaitEffectApplied)
            {
                SetCaptureChanceInstant(hitPosition, true);
            }
            else
            {
                Debug.Log($"{animalName} (Medium)은 미끼에 반응하지 않아 돌을 맞아도 포획 불가!");
            }
        }
        else if (animalSizeType == AnimalSizeType.Large)
        {
            Debug.Log($"{animalName} (Large)은 돌을 맞아도 포획할 수 없습니다.");
        }
    }

    public void TryGetHitByRock(Vector2 hitPosition)
    {
        if (BaitEffectApplied)
        {
            GetHitByRock(hitPosition);  
        }
        else
        {
            Debug.Log($"{animalName}은 미끼에 반응하지 않아서 돌에 맞아도 포획 불가!");
        }
    }

    void SetCaptureChanceInstant(Vector3 targetPosition, bool isBaitEffective)
    {
        if (!isBaitEffective)
        {
            captureChance = 0f;
            canBeCaptured = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, targetPosition);
        float bonus = Mathf.Clamp01(1f - distance / 5f);
        float bonusChance = bonus * 0.5f;

        captureChance = baseCaptureChance + bonusChance;
        canBeCaptured = true;
        Debug.Log($"{animalName} 포획 기회 활성화됨! 현재 확률: {captureChance * 100f}%");
    }

    IEnumerator CheckProximityAndIncreaseChance()
    {
        stayNearBaitTimer = 0f;

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
                    Debug.Log($"{animalName}가 {requiredStayTime}초 동안 미끼 근처에 머무름. 포획 확률 증가!");
                    captureChance += 0.2f;
                    captureChance = Mathf.Clamp01(captureChance);
                    break;
                }
            }
            else
            {
                stayNearBaitTimer = 0f; 
            }

            yield return null;
        }
    }

    void Flee()
    {
        if (isFleeing) return;
        isFleeing = true;
        Debug.Log($"{animalName}이(가) 도망갑니다!");
    }

    public bool TryCapture()
    {
        if (!canBeCaptured)
        {
            Debug.Log($"{animalName}은(는) 아직 포획할 수 없습니다.");
            return false;
        }

        float roll = Random.value;
        bool success = roll < captureChance;
        Debug.Log(success ? $"{animalName} 포획 성공!" : $"{animalName} 포획 실패!");

        return success;
    }

    public void ApplyBaitEffect()
    {
        BaitEffectApplied = true;
        Debug.Log($"{gameObject.name} : 미끼 효과 적용됨");
    }
}

