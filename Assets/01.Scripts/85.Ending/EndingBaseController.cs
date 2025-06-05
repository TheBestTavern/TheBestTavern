using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;


public abstract class EndingBaseController : MonoBehaviour
{
    [SerializeField] private EndingSceneUI ui;

    private void Start()
    {
        
    }

    protected async Task ShowText(string text)
    {
        //isTexting = true;
        //npcText.text = "";
        //SoundManager.Instance.PlaySFX("TutorialLine");
        for (int i = 0; i < text.Length; i++)
        {
            //npcText.text += text[i];
            await UniTask.WaitForSeconds(0.05f);
        }

        //isTexting = false;
    }
}
