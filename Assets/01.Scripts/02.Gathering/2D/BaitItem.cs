using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitItem : MonoBehaviour
{
    public BaitData baitData;

    private void Start()
    {
        // 동물 근처에 던졌다고 가정할 때, 근처 동물에게 알림
        Collider[] hitAnimals = Physics.OverlapSphere(transform.position, 5f); // 범위 탐색

        foreach (var hit in hitAnimals)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                animal.ReactToBait(baitData.baitType, transform.position);
            }
        }
    }
}
