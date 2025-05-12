using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{
    [Header("낚시 설정")]
    [SerializeField] private GameObject fishingUI;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private Transform fishSpawnArea;
    [SerializeField] private Transform catchZone;

    [Header("컨트롤러 설정")]
    [SerializeField] private TensionGaugeController tensionGaugeController;
    [SerializeField] private FishingLineController fishLineController;
    [SerializeField] private FishingBaitDrop fishingBaitDrop;

    private ItemStack currentBait;
    private GameObject currentFish;
    private bool fishingInProgress = false;

    private void Start()
    {
        fishingUI.SetActive(true);
        SetFishing();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !fishingInProgress && currentBait != null)
        {
            StartCoroutine(StartFishing());
        }

        if (fishingInProgress && currentFish != null)
        {
            var fishController = currentFish.GetComponent<FishController>();

            if (Input.GetKey(KeyCode.Space))
            {
                fishController.PullToward(catchZone.position);
                tensionGaugeController.IncreaseGauge();
            }
            else
            {
                tensionGaugeController.DecreaseGauge();
            }

            if (tensionGaugeController.IsOverloaded())
            {
                Debug.Log("게이지 과부하 실패");
                StopFishing(false);
            }
            else if (fishController.IsCaught(catchZone.position))
            {
                Debug.Log("물고기 성공");
                FishingSuccess();

            }
        }
    }

    private void SetFishing()
    {
        Vector3 spawnPos = fishSpawnArea.position;

        currentFish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);
        fishLineController.lineEndTarget = currentFish.transform;
        tensionGaugeController.ResetGauge();
    }

    IEnumerator StartFishing()
    {
        fishingInProgress = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        FishingManager.Instance.BeginFishing();
        if (currentBait != null)
        {
            fishingBaitDrop.ClearBait();
        }
    }

    private void StopFishing(bool success)
    {
        if (currentFish != null)
        {
            Destroy(currentFish);
        }
        tensionGaugeController.ResetGauge();
        fishingUI.SetActive(false);
        fishingInProgress = false;
    }

    private void FishingSuccess()
    {
        StopFishing(true);
        FishingManager.Instance.UnLoadMiniGame();
    }

    public void SetBait(ItemStack bait)
    {
        currentBait = bait;
        Debug.Log("미끼 설정: " + currentBait.Origin.englishName);
    }
}
