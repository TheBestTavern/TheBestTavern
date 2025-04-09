using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField] private BaitType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Animal animal = other.GetComponent<Animal>();
            if (animal != null)
            {
                animal.ReactToBait(type, transform);
                Destroy(gameObject); // 미끼는 사라짐
            }
        }
    }
}

public enum BaitType
{
    carrot,
    berry,
    fish
}
