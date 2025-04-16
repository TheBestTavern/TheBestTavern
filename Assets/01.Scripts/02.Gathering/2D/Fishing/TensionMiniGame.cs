using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionMiniGame : MonoBehaviour
{
    public RectTransform fishMarker;
    public RectTransform hookZone;
    public Image tensionBar;
    public float gameDuration = 5f;
    public float tensionGain = 0.5f;
    public float tensionLoss = 1f;
    public float markerSpeed = 200f;

    private RectTransform area;
    private float tension = 0.5f;
    private bool isPlaying = false;
    private System.Action<bool> onGameEnd;

    void Awake()
    {
        area = fishMarker.parent.GetComponent<RectTransform>();
    }

    public void StartMiniGame(FishData fish, System.Action<bool> callback)
    {
        tension = 0.5f;
        isPlaying = true;
        onGameEnd = callback;
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        float timer = 0f;
        float dir = 1f;

        while (timer < gameDuration)
        {
            // 물고기 이동
            Vector2 pos = fishMarker.anchoredPosition;
            pos.y += dir * markerSpeed * Time.deltaTime;

            // 상하 경계 체크
            float limitY = (area.rect.height - fishMarker.rect.height) / 2;
            if (Mathf.Abs(pos.y) > limitY)
            {
                pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
                dir *= -1;
            }

            fishMarker.anchoredPosition = pos;

            // HookZone 안에 있는지 확인
            if (RectOverlaps(fishMarker, hookZone))
                tension += tensionGain * Time.deltaTime;
            else
                tension -= tensionLoss * Time.deltaTime;

            tension = Mathf.Clamp01(tension);
            tensionBar.fillAmount = tension;

            // 실패 조건
            if (tension <= 0)
            {
                EndGame(false);
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // 성공 조건
        EndGame(true);
    }

    void EndGame(bool success)
    {
        isPlaying = false;
        onGameEnd?.Invoke(success);
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(b, a.position, null);
    }
}
