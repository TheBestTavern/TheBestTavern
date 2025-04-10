using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public GameObject throwObjectPrefab;
    public Transform throwPoint;
    public float maxPower = 10f;
    public float minAngle = -45f;
    public float maxAngle = 45f;

    float holdTime;
    float maxHoldTime = 2f;
    float currentDirection = 0f; // -1 ~ 1

    void Update()
    {
        // 방향 조절 (좌우 화살표 등으로 조절)
        currentDirection = Mathf.Sin(Time.time * 2f); // 좌우 왕복

        if (Input.GetMouseButtonDown(0)) holdTime = 0f;

        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0f, maxHoldTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            float power = (holdTime / maxHoldTime) * maxPower;
            float angle = Mathf.Lerp(minAngle, maxAngle, (currentDirection + 1f) / 2f);

            Throw(power, angle);
        }
    }

    void Throw(float power, float angle)
    {
        GameObject obj = Instantiate(throwObjectPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        // angle: y축 기준 회전 → 방향 벡터 계산
        Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward + Vector3.up;
        rb.AddForce(dir.normalized * power, ForceMode.Impulse);
    }
}
