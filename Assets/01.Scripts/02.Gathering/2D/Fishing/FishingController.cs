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

    private GameObject currentFish;
    private bool fishingInProgress = false;

    private void Start()
    {
        fishingUI.SetActive(true);
        SetFishing();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !fishingInProgress)
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
                FishingManager.Instance.success = false;
                FishingResult();
            }
            else if (fishController.IsCaught(catchZone.position))
            {
                Debug.Log("물고기 성공");
                FishingManager.Instance.success = true;
                FishingResult();
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

    private void FishingResult()
    {
        StopFishing(true);
        FishingManager.Instance.ShowResult();
    }
}
