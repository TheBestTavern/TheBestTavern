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

    private float baseCaptureChance = 0.2f; // 기본 확률 (20%)
    private float captureChance = 0f; // 실시간 계산된 포획 확률
    private bool canBeCaptured = false;

    public void ReactToBait(string baitType, Vector3 baitPosition)
    {
        bool likesBait = System.Array.Exists(favoriteBaits, bait => bait == baitType);

        switch (sizeType)
        {
            case AnimalSizeType.Small:
                SetCaptureChance(baitType, baitPosition, true);
                break;

            case AnimalSizeType.Medium:
                if (likesBait)
                {
                    SetCaptureChance(baitType, baitPosition, true);
                    MoveToBait(baitPosition);
                }
                break;

            case AnimalSizeType.Large:
                if (likesBait)
                {
                    Debug.Log($"{animalName}은(는) 미끼를 좋아하지만 의심이 많습니다. 도망 준비 중...");
                    Invoke(nameof(Flee), 3f); // 3초 후 도망
                }
                break;
        }
    }

    void SetCaptureChance(string baitType, Vector3 baitPosition, bool isBaitEffective)
    {
        if (!isBaitEffective)
        {
            captureChance = 0f;
            canBeCaptured = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, baitPosition);
        float bonus = Mathf.Clamp01(1f - distance / 5f); // 최대 거리 5 기준
        float bonusChance = bonus * 0.5f; // 최대 50% 보너스

        captureChance = baseCaptureChance + bonusChance;
        canBeCaptured = true;

        Debug.Log($"[동물: {animalName}] 미끼({baitType}) 반응함. 거리: {distance:F2}, 포획 확률 증가: +{bonusChance * 100:F1}%, 총 확률: {captureChance * 100:F1}%");
    }

    void MoveToBait(Vector3 baitPos)
    {
        transform.LookAt(baitPos);
        transform.position = Vector3.MoveTowards(transform.position, baitPos, 2f * Time.deltaTime);
    }

    void Flee()
    {
        Debug.Log($"{animalName}이(가) 도망쳤습니다!");
        Destroy(gameObject);
    }

    public bool CanBeCaptured()
    {
        return canBeCaptured;
    }

    public void TryCapture()
    {
        if (!canBeCaptured)
        {
            Debug.Log($"{animalName}은(는) 아직 포획할 수 없습니다.");
            return;
        }

        float roll = Random.value;
        bool success = roll < captureChance;

        Debug.Log($"[동물: {animalName}] 포획 시도! 확률: {captureChance * 100:F1}% → 랜덤값: {roll:F2} → {(success ? "성공!" : "실패")}");

        if (success)
        {
            Debug.Log($"{animalName} 포획 성공!");
            Destroy(gameObject);
        }
    }
}
