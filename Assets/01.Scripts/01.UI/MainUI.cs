using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Button cookingSceneButton;
    [SerializeField] private Button gatheringSceneButton;

    private void Awake()
    {
        cookingSceneButton.onClick.AddListener(OnClickCookingButton);
        gatheringSceneButton.onClick.AddListener(OnGatheringSceneButton);
    }

    void OnClickCookingButton()
    {
        _ = SceneLoader.Instace.LoadSceneAsync("CookingSceneDev");
    }

    void OnGatheringSceneButton()
    {
        _ = SceneLoader.Instace.LoadSceneAsync("GatheringSceneDev");
    }

}
