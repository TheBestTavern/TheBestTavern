using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public float activeTime = 5f;
    public float searchInterval = 0.5f;
    public float attractRadius = 10f;

    private bool isGrounded = false;

    void Start()
    {
        // Rigidbody 존재 시 땅에 닿은 이후 attract 시작
        StartCoroutine(CheckGrounded());
    }

    IEnumerator CheckGrounded()
    {
        while (!isGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f))
            {
                if (hit.collider.CompareTag("Ground")) // 땅 태그로 확인
                {
                    isGrounded = true;
                    Debug.Log("Ground");
                    StartCoroutine(AttractAnimals());
                }
            }
            yield return null;
        }
    }

    IEnumerator AttractAnimals()
    {
        float elapsed = 0f;

        while (elapsed < activeTime)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, attractRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Animal"))
                {
                    Animal animal = hit.GetComponent<Animal>();
                    if (animal != null)
                    {
                        animal.SetBait(transform);
                    }
                }
            }

            elapsed += searchInterval;
            yield return new WaitForSeconds(searchInterval);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractRadius);
    }
}

public enum BaitType
{
    carrot,
    berry,
    fish
}
