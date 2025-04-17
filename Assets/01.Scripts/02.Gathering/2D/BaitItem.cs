using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitItem : MonoBehaviour
{
    public BaitData baitData;

    private void Start()
    {
        Collider[] hitAnimals = Physics.OverlapSphere(transform.position, 5f);

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
