using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaitThrower2D : MonoBehaviour
{
    public GameObject baitPrefab;
    public Transform throwPoint;
    public float maxForce = 15f;
    public float minForce = 5f;
    public float forceSpeed = 2f;
    public float directionSpeed = 2f;

    public Transform directionArrow; // 방향 화살표 오브젝트
    public Image gaugeFillImage;     // UI 게이지 이미지

    private float currentForce;
    private bool increasingForce = true;

    private float currentDirection = 0f;
    private bool movingRight = true;

    void Update()
    {
        UpdateForceGauge();
        UpdateDirectionArrow();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBait();
        }
    }

    void UpdateForceGauge()
    {
        if (increasingForce)
        {
            currentForce += Time.deltaTime * forceSpeed;
            if (currentForce >= 1f)
            {
                currentForce = 1f;
                increasingForce = false;
            }
        }
        else
        {
            currentForce -= Time.deltaTime * forceSpeed;
            if (currentForce <= 0f)
            {
                currentForce = 0f;
                increasingForce = true;
            }
        }

        // 👉 UI 게이지 채우기
        if (gaugeFillImage != null)
        {
            gaugeFillImage.fillAmount = currentForce;
        }
    }

    void UpdateDirectionArrow()
    {
        if (movingRight)
        {
            currentDirection += Time.deltaTime * directionSpeed;
            if (currentDirection >= 1f)
            {
                currentDirection = 1f;
                movingRight = false;
            }
        }
        else
        {
            currentDirection -= Time.deltaTime * directionSpeed;
            if (currentDirection <= -1f)
            {
                currentDirection = -1f;
                movingRight = true;
            }
        }

        // 🔁 화살표 회전 - -45도(왼쪽) ~ +45도(오른쪽)
        if (directionArrow != null)
        {
            float angle = Mathf.Lerp(-45f, 45f, (currentDirection + 1f) / 2f);
            directionArrow.localRotation = Quaternion.Euler(45f, 0f, angle);
        }
    }

    void ThrowBait()
    {
        GameObject bait = Instantiate(baitPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = bait.GetComponent<Rigidbody>();

        float finalForce = Mathf.Lerp(minForce, maxForce, currentForce);

        // 방향 계산: 로컬 right 방향 (화면에서 보이는 좌우 기준)
        Vector3 direction = directionArrow.rotation * Vector3.right;

        rb.useGravity = true; // 또는 false, 원하는 느낌에 따라 조절
        rb.AddForce(direction * finalForce, ForceMode.Impulse);

        Debug.DrawRay(throwPoint.position, direction * 3f, Color.red, 2f);

        Destroy(bait, 10f);
    }
}
