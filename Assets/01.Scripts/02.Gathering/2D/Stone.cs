using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private float lifeTime = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger 충돌 감지됨: " + other.name);

        if (other.CompareTag("Animal"))
        {
            Debug.Log("Animal에 충돌함!");

            Animal animal = other.GetComponent<Animal>();
            if (animal != null)
            {
                animal.GetHitByRock(transform.position); 
                Debug.Log("동물 기절 처리 완료!");

                CaptureManager.Instance.ForceCheckAnimal(animal);
            }
            else
            {
                Debug.LogWarning("Animal 컴포넌트를 찾지 못함!");
            }

            Destroy(gameObject);  
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
