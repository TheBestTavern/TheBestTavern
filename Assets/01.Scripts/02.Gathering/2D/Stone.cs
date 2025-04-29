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
                animal.TryGetHitByRock(transform.position); // position 전달
                Debug.Log("동물 기절 처리 완료!");

                // ⭐ 포획 매니저에게 상태 업데이트 알리기
                CaptureManager.Instance.ForceCheckAnimal(animal);
            }
            else
            {
                Debug.LogWarning("Animal 컴포넌트를 찾지 못함!");
            }

            Destroy(gameObject);  // 돌 제거
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
