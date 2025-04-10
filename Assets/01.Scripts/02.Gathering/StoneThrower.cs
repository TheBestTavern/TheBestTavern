using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneThrower : MonoBehaviour
{
    [Header("게이지")]
    public Image powerSlider;
    public float maxPower = 100f;
    public float chargeSpeed = 50f;
    private float currentPower = 0f;

    public GameObject stonePrefab;
    public Transform throwPoint;
    public float minThrowForce = 5f;
    public float maxThrowForce = 20f;
    public float maxChargeTime = 2f;

    private float chargeTime = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            chargeTime = 0f;
        }

        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);

            // 파워 게이지 업데이트
            if (powerSlider != null)
            {
                powerSlider.fillAmount = chargeTime / maxChargeTime;
            }
        }

        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            ThrowStone();
            isCharging = false;

            // 파워 게이지 리셋
            if (powerSlider != null)
            {
                powerSlider.fillAmount = 0f;
            }
        }
    }

    void ThrowStone()
    {
        float powerPercent = chargeTime / maxChargeTime;
        float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, powerPercent);

        GameObject stone = Instantiate(stonePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = stone.GetComponent<Rigidbody>();

        // 카메라는 정면을 보므로 "앞으로"는 Z축 (+Z)
        Vector3 throwDirection = new Vector3(0f, 1f, 1f).normalized;
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        Destroy(stone, 3f); // 5초 후 파괴
    }
}
