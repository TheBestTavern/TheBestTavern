using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MileStoneController : MonoBehaviour
{
    [SerializeField] private Transform topStone;
    [SerializeField] private Transform handle; // 손잡이

    public float rotationSpeed = 140f;

    public bool isDragging = false;
    private Vector2 lastMousePos;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (IsMouseOverHandle())
            {
                isDragging = true;
                lastMousePos = Mouse.current.position.ReadValue();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 currentMousePos = Mouse.current.position.ReadValue();
            Vector2 centerScreenPos = Camera.main.WorldToScreenPoint(handle.position);

            Vector2 vecA = lastMousePos - centerScreenPos;
            Vector2 vecB = currentMousePos - centerScreenPos;

            float direction = Mathf.Sign((vecA.x * vecB.y) - (vecA.y * vecB.x)); // 외적
            float angle = Vector2.Angle(vecA, vecB);

            float rotationAmount = direction * angle * rotationSpeed * Time.deltaTime;
            topStone.Rotate(-Vector3.up, rotationAmount, Space.World);

            lastMousePos = currentMousePos;
        }
    }

    private bool IsMouseOverHandle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == handle;
        }
        return false;
    }
}
