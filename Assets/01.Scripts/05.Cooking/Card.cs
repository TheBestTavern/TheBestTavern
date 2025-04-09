using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 개별 카드에 필요한 Class
/// </summary>
public class Card : MonoBehaviour 
{
    public int idx = 0; // 카드의 짝 판별용 ID

    public GameObject front;
    public GameObject back;

    public TextMeshProUGUI numberText;

    public Button clickedCard;

    //public Animator animator;

    public CookingGrillMiniGame grillMiniGame;

 

    private void Awake()
    {
        clickedCard.onClick.AddListener(() =>
        {
            if (grillMiniGame.isFlipLocked) return;
            ToggleCard();
        });
    }
    
    public void Setting(int index)
    {
        idx = index;
        numberText.text = index.ToString();
    }

    public void ToggleCard()
    {
        bool isFront = front.activeSelf; // 현재 앞면이 보이고 있는지

        front.SetActive(!isFront); // 현재 뒷면이면 앞면 활성화
        back.SetActive(isFront); // 현재 앞면이면 뒷면 활성화

        grillMiniGame.OpenCard(this);
    }

    public bool IsMatch(Card firstCard, Card secondCard)
    {
        return firstCard.idx == secondCard.idx;
    }

}
