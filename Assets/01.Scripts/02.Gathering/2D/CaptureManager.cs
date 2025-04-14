using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureManager : MonoBehaviour
{
    public Button captureButton;  // Reference to the capture button
    public float captureRadius = 5f;  // Distance within which an animal can be captured
    private Animal animalInRange;


    void Start()
    {
        captureButton.onClick.AddListener(CaptureAnimal);  // Bind the button to the capture method
    }

    void Update()
    {
        // Check for animals in range every frame (or you could trigger this by specific conditions)
        CheckForAnimalsInRange();
    }

    // Check for animals within the capture radius
    void CheckForAnimalsInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, captureRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null && animal.CanBeCaptured())
            {
                animalInRange = animal;
                return;
            }
        }

        animalInRange = null;  // No animals in range
    }

    // Handle capture button press
    void CaptureAnimal()
    {
        if (animalInRange != null)
        {
            bool success = animalInRange.TryCapture();
            if (success)
            {
                Debug.Log("Animal successfully captured!");
            }
            else
            {
                Debug.Log("Capture failed: No animal in range or conditions not met.");
            }
        }
        else
        {
            Debug.Log("No animal in range to capture.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, captureRadius);
    }
}
