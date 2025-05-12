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
    public FishingLineController fishLineController;

    [SerializeField] private FishingBaitDrop fishingBaitDrop;
    private ItemStack currentBait;
    private GameObject baitObject;
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
                tensionGauge.IncreaseGauge();
            }
            else
            {
                tensionGauge.DecreaseGauge();
            }

            if (tensionGauge.IsOverloaded())
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

    public void SetFishing()
    {
        Vector3 spawnPos = fishSpawnArea.position;

        currentFish = Instantiate(fishPrefab, spawnPos, Quaternion.identity);
        fishLineController.lineEndTarget = currentFish.transform;
        tensionGauge.ResetGauge();
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

    void StopFishing(bool success)
    {
        if (currentFish != null)
        {
            Destroy(currentFish);
        }
        tensionGauge.ResetGauge();
        fishingUI.SetActive(false);
        fishingInProgress = false;
    }

    public void FishingSuccess()
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
