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

    float currentPower = 0f;  
    bool isIncreasing = true;  

    // UI Elements
    public Image powerUI;  

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isIncreasing)
            {
                currentPower += Time.deltaTime * maxPower;
                if (currentPower >= maxPower)
                {
                    currentPower = maxPower; 
                    isIncreasing = false;  
                }
            }
            else
            {
                currentPower -= Time.deltaTime * maxPower;
                if (currentPower <= 0f)
                {
                    currentPower = 0f;  
                    isIncreasing = true;  
                }
            }

            ShowTrajectory(currentPower);

            UpdatePowerUI(currentPower);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Throw(currentPower);  
            HidePreview();  
            currentPower = 0f; 
            UpdatePowerUI(currentPower);  
        }
    }

    void Throw(float power)
    {
        GameObject obj = Instantiate(throwObjectPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        float rad = throwAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);
    }

    void ShowTrajectory(float power)
    {
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

        lineRenderer.positionCount = previewResolution;
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }

    void HidePreview()
    {
        lineRenderer.enabled = false;
    }

    // Update the UI to show the power
    void UpdatePowerUI(float power)
    {
        if (powerUI != null)
        {
            powerUI.fillAmount = power / maxPower;  
        }
    }
}
