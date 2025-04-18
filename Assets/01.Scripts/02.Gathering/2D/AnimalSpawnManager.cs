using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
    public List<GameObject> smallAnimals;
    public List<GameObject> mediumAnimals;
    public List<GameObject> largeAnimals;
    public GameObject ground;

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
                animalToSpawn.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                break;

            case AnimalSizeType.Medium:
                animalToSpawn = mediumAnimals[Random.Range(0, mediumAnimals.Count)];
                animalToSpawn.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                break;

            case AnimalSizeType.Large:
                animalToSpawn = largeAnimals[Random.Range(0, largeAnimals.Count)];
                animalToSpawn.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                break;
        }

        if (animalToSpawn != null)
        {
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane + 5f));
            spawnPosition.z = 0; 

            Instantiate(animalToSpawn, spawnPosition, Quaternion.identity);
        }

        if (ground != null)
        {
            Vector3 offset = new Vector3(2f, 1f, 0f);
            Vector3 groundSpawn = Camera.main.transform.position + offset;
            groundSpawn.z = 0f;
            spawnPoint.position = groundSpawn;

            Instantiate(ground, spawnPoint);
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
