using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class MixingSpoon : MonoBehaviour
{
    [SerializeField] private Transform spoon;
    [SerializeField] private Transform bowl;
    [SerializeField] private Transform content;


    public bool isDragging = false;
    private Vector2 lastMousePos;
    private float rotationSpeed = 150; 

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
            spoon.DOKill();
        }

        if (isDragging)
        {
            Vector2 currentMousePos = Mouse.current.position.ReadValue();
            Vector2 centerScreenPos = Camera.main.WorldToScreenPoint(spoon.position);

            Vector2 vecA = lastMousePos - centerScreenPos;
            Vector2 vecB = currentMousePos - centerScreenPos;

            float direction = Mathf.Sign((vecA.x * vecB.y) - (vecA.y * vecB.x)); // 외적
            float angle = Vector2.Angle(vecA, vecB);

            float rotationAmount =  rotationSpeed * Time.deltaTime;

            spoon.RotateAround(bowl.position, Vector3.up, rotationAmount);

            content.Rotate(Vector3.up, rotationAmount * 0.1f);

            content.DOScale(new Vector3(0.85f, 0.15f, 0.85f), 0.3f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

            lastMousePos = currentMousePos;
        }
    }

    private bool IsMouseOverHandle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == spoon;
        }
        return false;
    }
}
