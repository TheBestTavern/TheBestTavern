using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI startText;

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

            if (fishingInProgress)
            {
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
                    SoundManager.Instance.PlaySFX("RodCut");
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
        int i = 3;
        while (i > -1)
        {
            if (i == 0)
            {
                startText.text = "시작";
            }
            else
            {
                startText.text = i.ToString();
            }
            yield return new WaitForSeconds(1);
            i--;
        }
        startText.gameObject.SetActive(false);
        fishingInProgress = true;
        SoundManager.Instance.PlaySFX("BaitSplash");
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
        FishingManager.Instance.FinishFishing();
    }

    private void FishingResult()
    {
        StopFishing(true);
        FishingManager.Instance.ShowResult();
    }
}
