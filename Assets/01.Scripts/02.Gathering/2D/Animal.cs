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
    public string[] favoriteBaits;

    private bool canBeCaptured = false;

    public void ReactToBait(string baitType, Vector3 baitPosition)
    {
        bool likesBait = System.Array.Exists(favoriteBaits, bait => bait == baitType);

        switch (sizeType)
        {
            case AnimalSizeType.Small:
                canBeCaptured = true;
                break;

            case AnimalSizeType.Medium:
                if (likesBait)
                {
                    canBeCaptured = true;
                    // bait 위치로 이동하는 연출
                    MoveToBait(baitPosition);
                }
                break;

            case AnimalSizeType.Large:
                if (likesBait)
                {
                    Invoke(nameof(Flee), 3f); // 3초 후 도망
                }
                break;
        }
    }

    void MoveToBait(Vector3 baitPos)
    {
        // 간단한 이동 처리
        transform.LookAt(baitPos);
        transform.position = Vector3.MoveTowards(transform.position, baitPos, 2f * Time.deltaTime);
    }

    void Flee()
    {
        Debug.Log(animalName + "이(가) 도망쳤습니다!");
        Destroy(gameObject);
    }

    public bool CanBeCaptured()
    {
        return canBeCaptured;
    }

    public void TryCapture()
    {
        if (canBeCaptured)
        {
            Debug.Log(animalName + " 포획 성공!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(animalName + "은(는) 아직 포획할 수 없습니다.");
        }
    }
}
