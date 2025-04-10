using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimalData
    {
        public GameObject prefab;
    }

    public AnimalData[] animalPrefabs; // 동물 프리팹 배열

    void Start()
    {
        SpawnRandomAnimal();
    }

    void SpawnRandomAnimal()
    {
        if (animalPrefabs.Length == 0)
        {
            Debug.LogWarning("동물 프리팹이 비어있습니다.");
            return;
        }

        int randIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[randIndex].prefab, transform.position, Quaternion.identity);
    }
}
