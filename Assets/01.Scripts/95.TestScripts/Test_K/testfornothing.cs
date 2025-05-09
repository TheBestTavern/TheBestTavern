using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class testfornothing : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    Sequence showSeq;
    Sequence hideSeq;

    [SerializeField] Image panel;
    private Vector3 panelOriginalPos;

    void Start()
    {
        btn1.onClick.AddListener(test1);
        btn2.onClick.AddListener(test2);

        panelOriginalPos = panel.transform.position;

        Tween t = transform.DOMove(panelOriginalPos, 1);

        showSeq = DOTween.Sequence();
        showSeq.Pause();
        showSeq.SetAutoKill(false);
        showSeq.AppendCallback(() =>
        {
            panel.transform.position = panelOriginalPos + new Vector3(-100, -100, 0);
            panel.transform.position = GetStartPosition(); 
        });
        showSeq.Join(transform.DOMove(panelOriginalPos, 1));
        showSeq.Join(t);



        hideSeq = DOTween.Sequence();
        hideSeq.Pause();
        hideSeq.SetAutoKill(false);
        hideSeq.AppendCallback(() =>
        {
            panel.transform.position = panelOriginalPos;
        });
        hideSeq.Join(panel.transform.DOMove(panelOriginalPos + new Vector3(-100, -100, 0), 1));
    }

    Vector3 GetStartPosition()
    {
        return transform.position;
    }

    void test1()
    {
        PlayeSeq(showSeq);
    }
    void test2()
    {
        PlayeSeq(hideSeq);
    }

    Sequence currentSeq;
    //private void PlayeSeq(Sequence sequence) // 제출 판넬, 인벤토리 나타남.
    //{
    //    currentSeq?.Pause();
    //    currentSeq = sequence;

    //    if (sequence.IsPlaying() || sequence.IsComplete())
    //    {
    //        currentSeq.Restart();
    //    }
    //    else
    //    {
    //        currentSeq.Play();
    //    }
    //}
    private void PlayeSeq(Sequence sequence) // 제출 판넬, 인벤토리 나타남.
    {
            currentSeq?.Pause();

        currentSeq = sequence;

        currentSeq.Restart();
    }
}
