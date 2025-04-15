using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    public GameObject fishingUI;
    public TensionMiniGame tensionMiniGame;

    public FishData[] possibleFishes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartFishing();
        }
    }

    void StartFishing()
    {
        fishingUI.SetActive(true);
    }

    IEnumerator HandleBite()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f)); // 입질 시간 랜덤

        FishData selectedFish = possibleFishes[Random.Range(0, possibleFishes.Length)];
        tensionMiniGame.StartMiniGame(selectedFish, (bool success) => {
            if (success)
            {
                Debug.Log($"잡은 물고기: {selectedFish.fishName}!");
                // 물고기 획득 처리
            }
            else
            {
                Debug.Log("물고기 도망감!");
            }

            fishingUI.SetActive(false);
        });
    }
}
