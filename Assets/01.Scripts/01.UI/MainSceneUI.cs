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
        cookingSceneButton.onClick.AddListener(OnClickCookingButton);
        gatheringSceneButton.onClick.AddListener(OnClickGatheringSceneButton);
    }

    void OnClickCookingButton()
    {
        _ = SceneLoader.Instance.LoadSceneAsync("CookingSceneDev");
    }

    void OnClickGatheringSceneButton()
    {
        _ = SceneLoader.Instance.LoadSceneAsync("GatheringSceneDev");
    }

}
