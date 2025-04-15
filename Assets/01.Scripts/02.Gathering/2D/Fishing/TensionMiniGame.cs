using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensionMiniGame : MonoBehaviour
{
    public RectTransform fish;
    public RectTransform targetZone;
    public float moveSpeed = 200f;
    public float requiredTime = 3f;

    float currentTime = 0f;
    Action<bool> onFinish;
    bool isPlaying = false;

    public void StartMiniGame(FishData fishData, Action<bool> callback)
    {
        onFinish = callback;
        currentTime = 0;
        isPlaying = true;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isPlaying) return;

        // 물고기 움직임
        fish.anchoredPosition += Vector2.right * Mathf.Sin(Time.time * 2f) * moveSpeed * Time.deltaTime;

        // 플레이어 입력 (좌우)
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        targetZone.anchoredPosition += (Vector2)input * moveSpeed * Time.deltaTime;

        float distance = Mathf.Abs(fish.anchoredPosition.x - targetZone.anchoredPosition.x);
        if (distance < 50f)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime * 2f;
        }

        if (currentTime >= requiredTime)
        {
            isPlaying = false;
            onFinish?.Invoke(true);
            gameObject.SetActive(false);
        }

        if (currentTime <= 0)
        {
            isPlaying = false;
            onFinish?.Invoke(false);
            gameObject.SetActive(false);
        }
    }
}
