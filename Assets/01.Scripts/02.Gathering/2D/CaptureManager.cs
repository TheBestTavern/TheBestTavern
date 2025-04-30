using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureManager : MonoSingleton<CaptureManager>
{
    public Button captureButton; 
    public Button escapeButton;  
    public float captureRadius = 5f;
    private float captureChance = 0f;

    private Animal animalInRange;

    private void Awake()
    {
        captureButton.onClick.AddListener(CaptureAnimal);
        escapeButton.onClick.AddListener(EscapeFromAnimal);
    }
    void Start()
    {
        captureButton.gameObject.SetActive(false);
        escapeButton.gameObject.SetActive(false);
    }

    void Update()
    {
    }

    void CheckForAnimalsInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, captureRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                Debug.Log("동물");
                animalInRange = animal;
                return;
            }
        }
        animalInRange = null;
    }

    public void CaptureButton()
    {
        captureButton.gameObject.SetActive(true);
        escapeButton.gameObject.SetActive(false);
    }

    // 포획 시도
    void CaptureAnimal()
    {
        CheckForAnimalsInRange();
        if (animalInRange == null)
        {
            Debug.Log("No animal in range.");
            return;
        }

        bool success = animalInRange.TryCapture();
        if (success)
        {
            Debug.Log("동물 포획 성공!");
            animalInRange.DestroyAnimal();
        }
        else
        {
            Debug.Log("동물 포획 실패");
        }
    }

    public void EscapeFromAnimal()
    {
        Debug.Log("Escaped from large animal.");
        // 씬 전환
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, captureRadius);
    }

    public void ForceCheckAnimal(Animal animal)
    {
        animalInRange = animal;
    }
}
