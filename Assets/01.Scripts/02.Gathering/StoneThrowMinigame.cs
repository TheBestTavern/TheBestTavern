using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneThrowMinigame : MonoBehaviour
{
    [Header("게이지")]
    public Image powerSlider;
    public float maxPower = 100f;
    public float chargeSpeed = 50f;
    private float currentPower = 0f;
    private bool isCharging = false;

    [Header("돌")]
    public GameObject stonePrefab;
    public Transform throwPoint;
    public float minThrowForce = 5f;
    public float maxThrowForce = 20f;
    public Vector3 throwDirection = new Vector3(0, 1, 0);

    private void Update()
    {
        // 누르기 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentPower = 0f;
            powerSlider.gameObject.SetActive(true);
        }

        // 누르고 있는 중 - 게이지 증가
        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            currentPower += chargeSpeed * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0f, maxPower);
            powerSlider.fillAmount = currentPower / maxPower;
        }

        // 누른 걸 떼면 발사
        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            ThrowStone();
            isCharging = false;
            powerSlider.fillAmount = 0;
            powerSlider.gameObject.SetActive(false);
        }
    }

    void ThrowStone()
    {
        float powerPercent = currentPower / maxPower;
        float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, powerPercent);

        GameObject stone = Instantiate(stonePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = stone.GetComponent<Rigidbody>();

        // 던지는 방향: 살짝 위쪽
        Vector3 direction = throwDirection.normalized;
        rb.AddForce(direction * throwForce, ForceMode.Impulse);

        Debug.Log($"돌 던지기! 파워: {powerPercent * 100f:F1}% 힘: {throwForce:F1}");
    }
}
