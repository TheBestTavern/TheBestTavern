using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalSizeType
{
    Small,
    Medium,
    Large,
}

[RequireComponent(typeof(CaptureHandler))]
public class Animal : MonoBehaviour
{
    public string animalName;
    public AnimalSizeType sizeType;

    public bool isStunned = false;
    public bool isCaptured = false;

    // 좋아하는 미끼 (중형만 설정)
    public string favoriteBait;

    public void TryCapture(string baitUsed, bool usedRock)
    {
        switch (sizeType)
        {
            case AnimalSizeType.Small:
                // 무조건 포획 가능
                Capture();
                break;

            case AnimalSizeType.Medium:
                // 좋아하는 미끼 또는 기절 상태면 포획 가능
                if ((baitUsed == favoriteBait) || usedRock || isStunned)
                {
                    Capture();
                }
                else
                {
                    Debug.Log("포획 실패: 중형 동물은 좋아하는 미끼가 필요하거나 기절시켜야 합니다.");
                }
                break;

            case AnimalSizeType.Large:
                // 포획 불가능
                Debug.Log("대형 동물은 포획이 불가능합니다. 도망가기를 사용하세요.");
                break;
        }
    }

    public void Stun()
    {
        if (sizeType == AnimalSizeType.Medium)
        {
            isStunned = true;
            Debug.Log($"{animalName}이(가) 기절했습니다!");
        }
    }

    void Capture()
    {
        isCaptured = true;
        Debug.Log($"{animalName}이(가) 포획되었습니다!");
        // 이후에 포획 성공 UI나 애니메이션 등을 호출할 수 있어요.
        Destroy(gameObject);
    }
}
