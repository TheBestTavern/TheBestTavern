using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneThrowerController : MonoBehaviour
{
    [Header("프리팹 및 위치 설정")]
    [SerializeField] private GameObject throwObjectPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Camera miniGameCamera;
    [SerializeField] private Image powerUI;


    [Header("파워 및 각도 설정")]
    [SerializeField] private float maxPower;
    [SerializeField] private float throwAngle;
    [SerializeField] private float previewLength = 0.2f;
    [SerializeField] private int previewResolution = 30;

    private float currentPower = 0f;
    private bool isIncreasing = false;

    private void Start()
    {
        Vector3 bottomLeft = miniGameCamera.ViewportToWorldPoint(new Vector3(-0.9f, -0.9f, miniGameCamera.nearClipPlane + 5f));
        bottomLeft.z = 0f;
        throwPoint.position = bottomLeft;
    }

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
        SoundManager.Instance.PlaySFX("ThrowStone");
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

    void UpdatePowerUI(float power)
    {
        if (powerUI != null)
        {
            powerUI.fillAmount = power / maxPower;  
        }
    }
}
