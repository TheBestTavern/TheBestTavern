using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GatheringProps : MonoBehaviour
{
    bool isClicked = false;

    private void OnMouseEnter()
    {
        if (!isClicked)
            gameObject.transform.DOScale(1.05f, 0.5f);
    }

    private void OnMouseExit()
    {
        if (!isClicked)
            gameObject.transform.DOScale(1, 0.5f);
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            gameObject.transform.DOShakeScale(1f, 0.1f);
            int randint = Random.Range(0, 10);
            if (randint == 0)
            {
                OnMiniGame();
            }
            else
            {
                GetItem();
            }
            gameObject.transform.DOScale(1, 0.5f);
            isClicked = true;
        }
    }

    public void GetItem()
    {
        Debug.Log("아이템 획득");
    }

    async void OnMiniGame()
    {
        await SceneLoader.Instance.LoadSceneAsyncMiniGame("Forest_Animal");
    }
}
