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
    public bool BaitEffectApplied = false;

    private float baseCaptureChance = 0.2f;
    public float captureChance = 0f;
    private bool canBeCaptured = false;

    private void Start()
    {
    }
    public void ReactToBait(string baitType, Vector3 baitPosition)
    {
        foreach (string favorite in favoriteBaits)
        {
            if (favorite == baitType)
            {
                ApplyBaitEffect();
                Debug.Log($"{animalName}가 {baitType} 미끼에 반응함!");
                break;
            }
        }
    }

    // 돌에 맞았을 때 호출
    public void GetHitByRock(Vector3 hitPosition)
    {
        if (animalSizeType == AnimalSizeType.Small)
        {
            HitSmallAnimal();
        }
        else if (animalSizeType == AnimalSizeType.Medium)
        {
            if (BaitEffectApplied)
            {
                HitMediumAnimal(hitPosition);
            }
            else
            {
                Debug.Log($"{animalName} (Medium)은 미끼에 반응하지 않아 돌을 맞아도 포획 불가!1111");
            }
        }
        else if (animalSizeType == AnimalSizeType.Large)
        {
            Debug.Log($"{animalName} (Large)은 돌을 맞아도 포획할 수 없습니다.");
        }
    }

    public void HitSmallAnimal()
    {
        canBeCaptured = true;
        captureChance = 1f;
        CaptureManager.Instance.CaptureButton();
    }

    public void HitMediumAnimal(Vector3 hitPosition)
    {
        SetCaptureChanceInstant(hitPosition, true);
        CaptureManager.Instance.CaptureButton();
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

    public bool TryCapture()
    {
        if (!canBeCaptured)
        {
            Debug.Log($"{animalName}은(는) 아직 포획할 수 없습니다.");
            return false;
        }

        float randomValue = Random.value;
        bool success = randomValue < captureChance;
        Debug.Log(success ? $"{animalName} 포획 성공!" : $"{animalName} 포획 실패!");

        return success;
    }

    public void ApplyBaitEffect()
    {
        BaitEffectApplied = true;
    }

    public void DestroyAnimal()
    {
        Destroy(gameObject);
    }
}

