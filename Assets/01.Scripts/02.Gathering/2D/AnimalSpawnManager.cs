using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
    public List<GameObject> smallAnimals;
    public List<GameObject> mediumAnimals;
    public List<GameObject> largeAnimals;

    public Transform spawnPoint;

    void Start()
    {
        SpawnRandomAnimal();
    }

    public void SpawnRandomAnimal()
    {
        AnimalSizeType selectedSize = GetRandomSizeByProbability();

        GameObject animalToSpawn = null;

        switch (selectedSize)
        {
            case AnimalSizeType.Small:
                animalToSpawn = smallAnimals[Random.Range(0, smallAnimals.Count)];
                break;

            case AnimalSizeType.Medium:
                animalToSpawn = mediumAnimals[Random.Range(0, mediumAnimals.Count)];
                break;

            case AnimalSizeType.Large:
                animalToSpawn = largeAnimals[Random.Range(0, largeAnimals.Count)];
                break;
        }

        if (animalToSpawn != null)
        {
            Instantiate(animalToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }

    private AnimalSizeType GetRandomSizeByProbability()
    {
        float rand = Random.Range(0f, 100f);

        if (rand < 55f)
            return AnimalSizeType.Small;
        else if (rand < 90f)
            return AnimalSizeType.Medium;
        else
            return AnimalSizeType.Large;
    }
}
