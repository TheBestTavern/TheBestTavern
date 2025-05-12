using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    [Header("미끼 종류 및 효과")]
    [SerializeField] private float effectRadius = 2f;
    [SerializeField] private float lifetime = 3f;

    private int baitKey;
    private bool hasLanded = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PhysicsMaterial2D noSlide = new PhysicsMaterial2D
        {
            friction = 1f,
            bounciness = 0f
        };

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.sharedMaterial = noSlide;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasLanded && collision.CompareTag("Ground"))
        {
            Land();
        }
    }
    void Land()
    {
        hasLanded = true;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        rb.isKinematic = true;

        NotifyAnimals();
    }

    void NotifyAnimals()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                animal.ReactToBait(baitKey, transform.position);
            }
        }
    }

    public void SetBaitKey(int key)
    {
        baitKey = key;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}
