using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private Button cookingSceneButton;
    [SerializeField] private Button gatheringSceneButton;

    private void Awake()
    {
        cookingSceneButton.onClick.AddListener(async ()=> await SceneLoader.Instance.LoadSceneAsync("CookingSceneDev"));
        gatheringSceneButton.onClick.AddListener(() => UIManager.Instance.ShowPopUp(PopUpType.SelectMap));
    }
}
