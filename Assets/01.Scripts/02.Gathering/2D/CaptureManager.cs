using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureManager : MonoSingleton<CaptureManager>
{
    public Button captureButton; // 포획 버튼
    public Button escapeButton;  // 도망가기 버튼 (큰 동물용)
    public float captureRadius = 5f;

    private Animal animalInRange;

    void Start()
    {
        captureButton.onClick.AddListener(CaptureAnimal);
        escapeButton.onClick.AddListener(EscapeFromAnimal);
    }

    void Update()
    {
        CheckForAnimalsInRange();
        UpdateButtonVisibility();
    }

    // 범위 내 동물 확인
    void CheckForAnimalsInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, captureRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                animalInRange = animal;
                return;
            }
        }

        animalInRange = null;
    }

    // 버튼 활성화 조건 결정
    void UpdateButtonVisibility()
    {
        if (animalInRange == null)
        {
            captureButton.gameObject.SetActive(false);
            escapeButton.gameObject.SetActive(false);
            return;
        }

        switch (animalInRange.animalSizeType)
        {
            case AnimalSizeType.Small:
                // 기절 상태일 때만 포획 버튼 활성화
                captureButton.gameObject.SetActive(animalInRange.IsStunned);
                escapeButton.gameObject.SetActive(false);
                break;

            case AnimalSizeType.Medium:
                // 돌로 데미지 입힌 후, 조건 만족 시 포획 가능
                captureButton.gameObject.SetActive(animalInRange.IsStunned && animalInRange.BaitEffectApplied);
                escapeButton.gameObject.SetActive(false);
                break;

            case AnimalSizeType.Large:
                // 포획 불가, 도망만 가능
                captureButton.gameObject.SetActive(false);
                escapeButton.gameObject.SetActive(true);
                break;
        }
    }

    // 포획 시도
    void CaptureAnimal()
    {
        if (animalInRange == null)
        {
            Debug.Log("No animal in range.");
            return;
        }

        bool success = animalInRange.TryCapture();
        if (success)
        {
            Debug.Log("Animal captured!");
        }
        else
        {
            Debug.Log("Capture failed.");
        }
    }

    // 도망가기
    public void EscapeFromAnimal()
    {
        Debug.Log("Escaped from large animal.");
        // 씬 전환 및 화면 복귀 호출
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, captureRadius);
    }
}
