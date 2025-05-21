using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoSingleton<AnimalSpawnManager>
{
    [Header("동물 생성 위치")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Camera miniGameCamera;

    [Header("동물 프리팹")]
    [SerializeField] private List<GameObject> smallAnimals;
    [SerializeField] private List<GameObject> mediumAnimals;
    [SerializeField] private List<GameObject> largeAnimals;

    [Header("땅 프리팹")]
    [SerializeField] private GameObject ground;

    private GameObject animalToSpawn;

    

    void Start()
    {
        SpawnRandomAnimal();
    }

    public void SpawnRandomAnimal()
    {
        AnimalSizeType selectedSize = GetRandomSizeByProbability();

        animalToSpawn = null;

        switch (selectedSize)
        {
            case AnimalSizeType.Small:
                SpawnSmallAnimal();
                break;

            case AnimalSizeType.Medium:
                SpawnMediumAnimal();
                break;

            case AnimalSizeType.Large:
                SpawnLargeAnimal();
                break;
        }
        
        if (animalToSpawn != null)
        {
            AnimalSpawnPosition();
        }

        if (ground != null)
        {
            GroundSpawnPosition();
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

    private void SpawnSmallAnimal()
    {
        animalToSpawn = smallAnimals[Random.Range(0, smallAnimals.Count)];
        animalToSpawn.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
    }

    private void SpawnMediumAnimal()
    {
        animalToSpawn = mediumAnimals[Random.Range(0, mediumAnimals.Count)];
        animalToSpawn.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
    }

    private void SpawnLargeAnimal()
    {
        animalToSpawn = largeAnimals[Random.Range(0, largeAnimals.Count)];
        animalToSpawn.transform.localScale = new Vector3(1f, 1f, 1f);
        SoundManager.Instance.PlaySFX("Monster");
    }

    private void AnimalSpawnPosition()
    {
        Vector3 spawnPosition = miniGameCamera.ViewportToWorldPoint(new Vector3(1.5f, 1.5f, Camera.main.nearClipPlane + 5f));
        spawnPosition.z = 0;

        Instantiate(animalToSpawn, spawnPosition, Quaternion.identity);
    }

    private void GroundSpawnPosition()
    {
        Vector3 offset = new Vector3(1.5f, 1.2f, 0f);
        Vector3 groundSpawn = miniGameCamera.transform.position + offset;
        groundSpawn.z = 0f;
        spawnPoint.position = groundSpawn;

        Instantiate(ground, spawnPoint);
    }
}
