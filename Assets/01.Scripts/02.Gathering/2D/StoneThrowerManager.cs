using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneThrowerManager : MonoBehaviour
{
    public GameObject throwObjectPrefab;
    public Transform throwPoint;
    public LineRenderer lineRenderer;

    public float maxPower = 20f;
    public float throwAngle = 45f;
    public float previewLength = 0.2f;
    public int previewResolution = 30;

    float currentPower = 0f;  // Store current power
    bool isIncreasing = true;  // To track if the power is increasing or decreasing

    // UI Elements
    public Image powerUI;  // Use an Image for visual power bar

    void Update()
    {
        // While the Space key is held down, update the power
        if (Input.GetKey(KeyCode.Space))
        {
            if (isIncreasing)
            {
                // Increase the power until maxPower
                currentPower += Time.deltaTime * maxPower;
                if (currentPower >= maxPower)
                {
                    currentPower = maxPower;  // Cap at maxPower
                    isIncreasing = false;  // Switch to decreasing
                }
            }
            else
            {
                // Decrease the power to 0
                currentPower -= Time.deltaTime * maxPower;
                if (currentPower <= 0f)
                {
                    currentPower = 0f;  // Cap at 0
                    isIncreasing = true;  // Switch to increasing
                }
            }

            // Show the trajectory preview with the current power
            ShowTrajectory(currentPower);

            // Update the power UI display
            UpdatePowerUI(currentPower);
        }

        // Throw the stone once the Space key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Throw(currentPower);  // Throw the stone with the calculated power
            HidePreview();  // Hide the trajectory preview after throwing
            currentPower = 0f;  // Reset the power after throwing
            UpdatePowerUI(currentPower);  // Reset the UI display after throwing
        }
    }

    void Throw(float power)
    {
        // Instantiate the stone object and apply force based on the calculated power
        GameObject obj = Instantiate(throwObjectPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // Calculate the throw direction based on the throw angle
        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        // Apply the force to the Rigidbody2D object
        rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);
    }

    void ShowTrajectory(float power)
    {
        // Calculate the trajectory preview
        Vector3[] points = new Vector3[previewResolution];
        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        Vector3 startPos = throwPoint.position;
        Vector2 velocity = dir * power;

        for (int i = 0; i < previewResolution; i++)
        {
            float t = i * previewLength / previewResolution;
            Vector2 pos = (Vector2)startPos + velocity * t + 0.5f * Physics2D.gravity * t * t;
            points[i] = pos;
        }

        // Update the line renderer with the calculated trajectory points
        lineRenderer.positionCount = previewResolution;
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }

    void HidePreview()
    {
        // Disable the trajectory preview after the stone is thrown
        lineRenderer.enabled = false;
    }

    // Update the UI to show the power
    void UpdatePowerUI(float power)
    {
        if (powerUI != null)
        {
            powerUI.fillAmount = power / maxPower;  // Update the Slider UI (normalize to 0-1 range)
        }
    }
}
