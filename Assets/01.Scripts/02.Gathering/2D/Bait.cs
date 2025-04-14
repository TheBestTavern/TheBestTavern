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

        PhysicsMaterial2D noSlide = new PhysicsMaterial2D("NoSlide");
        noSlide.friction = 1f;
        noSlide.bounciness = 0f;

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.sharedMaterial = noSlide;

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
        Debug.Log("미끼가 착지함!");
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
        Debug.Log("미끼가 착지하여 동물에게 반응을 알림 시작");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                Debug.Log($"{animal.gameObject.name}에게 미끼 반응 전달");
                animal.ReactToBait(baitType, transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}
