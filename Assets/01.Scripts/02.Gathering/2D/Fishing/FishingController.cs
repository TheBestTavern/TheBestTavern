using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{
    public GameObject fishingUI;
    public GameObject fishPrefab;
    public Transform fishSpawnArea;
    public Transform catchZone;
    public TensionGauge tensionGauge;

    private GameObject currentFish;
    private bool fishingInProgress = false;

    private void Start()
    {
        fishingUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !fishingInProgress)
        {
            fishingUI.SetActive(true);
            StartCoroutine(StartFishing());
        }

        if (fishingInProgress && currentFish != null)
        {
            var fishController = currentFish.GetComponent<FishController>();

            if (Input.GetKey(KeyCode.Space))
            {
                fishController.PullToward(catchZone.position);
                tensionGauge.IncreaseGauge();
            }
            else
            {
                tensionGauge.DecreaseGauge();
            }

            if (tensionGauge.IsOverloaded())
            {
                Debug.Log("게이지 과부하! 실패!");
                StopFishing(false);
            }
            else if (fishController.IsCaught(catchZone.position))
            {
                Debug.Log("물고기 잡았다!");
                StopFishing(true);
            }
        }
    }

    IEnumerator StartFishing()
    {
        fishingInProgress = true;
        fishingUI.SetActive(true);

        yield return new WaitForSeconds(Random.Range(1f, 3f)); // 입질 기다림

        // 물고기 생성 위치: 수직으로 약간 랜덤하게 설정
        Vector3 spawnPos = fishSpawnArea.position;
        spawnPos.y += Random.Range(-2f, 2f);

        currentFish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);
        tensionGauge.ResetGauge(); // 새 시도니까 초기화
    }

    void StopFishing(bool success)
    {
        if (currentFish != null)
            Destroy(currentFish);

        tensionGauge.ResetGauge();
        fishingUI.SetActive(false);
        fishingInProgress = false;
    }
}
