using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CaptureManager : MonoSingleton<CaptureManager>
{
    [Header("포획 및 도망 버튼")]
    [SerializeField] private Button captureButton;
    [SerializeField] private Button escapeButton;
    [SerializeField] private Button infoButton;

    [Header("포획 설정")]
    [SerializeField] private float captureRadius = 5f;
    [SerializeField] public BaitDropArea baitDropArea;
    private Animal animalInRange;
    public bool success;

    protected override void Awake()
    {
        captureButton.onClick.AddListener(CaptureAnimal);
        escapeButton.onClick.AddListener(OnClickEscapeFromAnimal);
        infoButton.onClick.AddListener(OnClickInfoButton);
    }
    void Start()
    {
        captureButton.gameObject.SetActive(false);
        escapeButton.gameObject.SetActive(true);
        CheckForAnimalsInRange();
    }


    private void AddItem()
    {
        if (InventoryManager.Instance.Invens[InvenType.Gathering].아이템획득(Data.GetRawItem(animalInRange.gatheringKey), animalInRange.gatheringValue))
        {
            Debug.Log("아이템 증가");
        }
        else
        {
            Debug.Log("아이템 증가 불가능");
        }
    }

    private void CheckForAnimalsInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, captureRadius);
        foreach (var hit in hitColliders)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
            {
                Debug.Log("동물");
                animalInRange = animal;
                return;
            }
        }
        animalInRange = null;
    }

    public void OnClickCaptureButton()
    {
        captureButton.gameObject.SetActive(true);
    }

    private async void OnClickInfoButton()
    {
        await PopUpManager.Instance.ShowPopUp(PopUpType.GatheringInfo);
    }

    // 포획 시도
    private void CaptureAnimal()
    {
        CheckForAnimalsInRange();
        if (animalInRange == null)
        {
            Debug.Log("주변 동물 없음");
            return;
        }

        bool canCapture = animalInRange.TryCapture();
        if (canCapture)
        {
            success = true;
            Debug.LogError("동물 포획 성공");
            AddItem();
            animalInRange.DestroyAnimal();
            ShowResult();
        }
        else
        {
            Debug.LogError("동물 포획 실패");
            animalInRange.gatheringKey = 0;
            success = false;
        }
    }

    async void ShowResult()
    {
        await PopUpManager.Instance.ShowPopUp(PopUpType.GatheringResult);
    }

    public int GetItemKey()
    {
        return animalInRange.gatheringKey;
    }

    public bool GetResult()
    {
        return success;
    }

    private void OnClickEscapeFromAnimal()
    {
        if (animalInRange.animalSizeType == AnimalSizeType.Large)
        {
            if (!animalInRange.BaitEffectApplied)
            {
                ForestGatheringManager.Instance.gatheringInventoryUI.LoseAllItem();
            }
        }
        Debug.Log("도망가기");
        animalInRange.DestroyAnimal();
        UnLoadMiniGame();
    }

    async public void UnLoadMiniGame()
    {
        UIManager.Instance.gatheringSceneUI.SetMiniGameBackGround(false);
        await SceneLoader.Instance.UnLoadSceneAsyncMiniGame();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, captureRadius);
    }

    public void ForceCheckAnimal(Animal animal)
    {
        animalInRange = animal;
    }
}
