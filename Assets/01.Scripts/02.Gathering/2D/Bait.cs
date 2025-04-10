using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Bait : MonoBehaviour
{
    public string baitType;
    public float effectRadius = 5f;
    public float lifetime = 5f;

    private bool hasLanded = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 물리 소재 설정 (미끄러짐 방지)
        PhysicMaterial noSlide = new PhysicMaterial
        {
            name = "NoSlide",
            dynamicFriction = 1f,
            staticFriction = 1f,
            frictionCombine = PhysicMaterialCombine.Maximum,
            bounceCombine = PhysicMaterialCombine.Minimum,
            bounciness = 0f
        };

        Collider col = GetComponent<Collider>();
        col.material = noSlide;

        // Rigidbody 설정도 미리 해주기 (공중에서 천천히 떨어지도록)
        rb.drag = 5f;
        rb.angularDrag = 5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && collision.gameObject.CompareTag("Ground"))
        {
            Land();
        }
    }

    void Land()
    {
        hasLanded = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        // 필요하다면 위치 정밀 고정
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        NotifyAnimals();
        Destroy(gameObject, lifetime);
    }

    void NotifyAnimals()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius);
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
