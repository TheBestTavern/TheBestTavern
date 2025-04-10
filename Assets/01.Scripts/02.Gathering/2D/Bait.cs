using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public string baitType;
    public float effectRadius = 5f;
    public float lifetime = 5f;

    private bool hasLanded = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 물리 소재 설정 (미끄러짐 방지)
        PhysicsMaterial2D noSlide = new PhysicsMaterial2D("NoSlide");
        noSlide.friction = 1f;
        noSlide.bounciness = 0f;

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.sharedMaterial = noSlide;

        // 공중에서 느리게 떨어지도록 설정
        rb.drag = 0f;
        rb.angularDrag = 0.05f;
        rb.gravityScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.collider.name + " (Tag: " + collision.collider.tag + ")");

        if (!hasLanded && collision.collider.CompareTag("Ground"))
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
        Destroy(gameObject, lifetime);
    }

    void NotifyAnimals()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                animal.ReactToBait(baitType, transform.position);
            }
        }
    }
}
