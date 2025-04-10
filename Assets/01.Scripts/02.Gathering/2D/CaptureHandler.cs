using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureHandler : MonoBehaviour
{
    private Animal animal;

    private void Awake()
    {
        animal = GetComponent<Animal>();
    }

    public void TryCapture(BaitItem bait)
    {
        float chance = bait.captureChance;
        float roll = Random.value;

        Debug.Log($"[포획 시도] 확률: {chance}, 주사위: {roll}");

        if (roll < chance)
        {
        }
        else
        {
        }
    }
}
