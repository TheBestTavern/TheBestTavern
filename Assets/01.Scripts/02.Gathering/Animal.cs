using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [Header("미끼 관련")]
    [SerializeField] private List<BaitType> favoriteBaits;
    [SerializeField] private float catchSuccessRate = 0.3f; // 포획 확률
    [SerializeField] private float catchDistance = 1.5f; // 포획 시도 거리

    private NavMeshAgent agent;
    private Transform baitTarget;
    private bool isApproaching = false;
    private bool hasReacted = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isApproaching && baitTarget != null)
        {
            float distance = Vector3.Distance(transform.position, baitTarget.position);
            if (distance <= catchDistance)
            {
                isApproaching = false;
                agent.isStopped = true;
                TryCatch();
            }
        }
    }

    public void ReactToBait(BaitType baitType, Transform baitTransform)
    {
        if (hasReacted) return;

        if (favoriteBaits.Contains(baitType))
        {
            Debug.Log($"{gameObject.name}는 {baitType} 미끼를 좋아해서 다가간다!");
            baitTarget = baitTransform;
            isApproaching = true;
            hasReacted = true;

            agent.SetDestination(baitTransform.position);
        }
        else
        {
            Debug.Log($"{gameObject.name}는 {baitType} 미끼에 관심 없음.");
        }
    }

    private void TryCatch()
    {
        float roll = Random.Range(0f, 1f);
        Debug.Log($"{gameObject.name} 포획 시도! 확률: {catchSuccessRate * 100}% -> 주사위: {roll}");

        if (roll <= catchSuccessRate)
        {
            Debug.Log($"{gameObject.name} 포획 성공!");
            gameObject.SetActive(false); // 포획 성공 시 제거
        }
        else
        {
            Debug.Log($"{gameObject.name} 포획 실패...");
            // 실패 시 도망가기 같은 연출 가능
        }
    }
}
