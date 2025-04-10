using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectRange = 15f;
    public float stopDistance = 0.3f;

    private Transform baitTarget;
    private bool isMovingToBait = false;

    void Update()
    {
        if (baitTarget != null && isMovingToBait)
        {
            float distance = Vector3.Distance(transform.position, baitTarget.position);
            if (distance > stopDistance)
            {
                Vector3 dir = (baitTarget.position - transform.position).normalized;
                transform.position += dir * moveSpeed * Time.deltaTime;
            }
            else
            {
                isMovingToBait = false;
                Debug.Log("미끼 도착!");
            }
        }
    }

    public void SetBait(Transform bait)
    {
        Debug.Log("SetBait called");

        if (baitTarget == null)
        {
            float distance = Vector3.Distance(transform.position, bait.position);
            Debug.Log("Distance to bait: " + distance);

            if (distance <= detectRange)
            {
                Debug.Log("Animal started moving toward bait.");
                baitTarget = bait;
                isMovingToBait = true;
            }
            else
            {
                Debug.Log("Bait is out of range.");
            }
        }
        else
        {
            Debug.Log("Already has a bait target.");
        }
    }
}
