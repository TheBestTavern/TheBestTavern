using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitThrower : MonoBehaviour
{
    [Header("미끼")]
    public GameObject baitPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public Vector3 leftThrowDirection = new Vector3(-1, 0.5f, 0.5f);
    public Vector3 rightThrowDirection = new Vector3(1, 0.5f, 0.5f);

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ThrowBait(leftThrowDirection);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ThrowBait(rightThrowDirection);
        }
    }

    void ThrowBait(Vector3 direction)
    {
        GameObject bait = Instantiate(baitPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = bait.GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * throwForce, ForceMode.Impulse);
    }
}
