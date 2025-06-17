using System;
using System.Collections;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    TutorialManager manager;

    [SerializeField] Button ReceiveStep;
    [SerializeField] Button CancelTutorial;

    [SerializeField] TextMeshProUGUI chatTMP;
    [SerializeField] float typeInterval = 0.05f;
    Coroutine TypingCoroutine;
    Action chatAction;
    bool isTyping = false;
    bool isChatting = false;
    bool ready2nextStepChat =false ;
    string remain;
    [SerializeField] int lineSize = 2;
    string prevChar = "";

    private void Awake()
    {
        manager = TutorialManager.Instance;


        CancelTutorial.onClick.AddListener(async () =>
        {
            await PopUpManager.Instance.ShowPopUp(PopUpType.Confirm);
            PopUpManager.Instance.confirmPopUp.SetConfirm("한번 튜토리얼을 취소하면 다시 받을 수 없습니다. \n 정말 튜토리얼을 취소하시겠습니까? ", () => TutorialManager.Instance.QuitTutorial(TtrState.Cancelled));
        });

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isChatting && isTyping)
            {
                TypeImmediately();
            }
            else if (isChatting && !isTyping && !ready2nextStepChat)
            {
                GoNextPage();
            }
            else if(isChatting && ready2nextStepChat)
            {
                // 다음 스텝의 givingChat 시작하기
                ready2nextStepChat = false;
                StartGivingChat(manager.nextStepID.Value);
            }
        }
    }

    public void StartConversation(int? ttrID)
    {
        if (ttrID == null)
        {
            StartGivingChat(910001);
        }
        else
        {
            StartFollowingChat(ttrID.Value);
        }
    }

    void StartFollowingChat(int ttrID)
    {
        gameObject.SetActive(true);
        string dialogue = manager.GetTtrStepDef(ttrID).FollowingText;

        isChatting = true;
        chatAction = () => ClearCurStep(ttrID);
        ShowPage(dialogue);
    }

    void ClearCurStep(int stepID)
    {
        // 현재 스텝 완료 처리하기
        if (manager.GetCurTtrStepDef() != null)
            manager.ClearStep();

        ready2nextStepChat = true;
    }

    void StartGivingChat(int ttrID)
    {
        gameObject.SetActive(true);
        string dialogue = manager.GetTtrStepDef(ttrID).GivingText;

        isChatting = true;
        chatAction = () => ShowAcceptNextStep(ttrID);
        ShowPage(dialogue);
    }

    void ShowAcceptNextStep(int nextStepID)
    {
        // 새로운 스텝 수락
        ReceiveStep.onClick.RemoveAllListeners();
        ReceiveStep.onClick.AddListener(() =>
        {
            manager.AcceptNewStep(nextStepID);
            EndConversation();
        });

        ReceiveStep.gameObject.SetActive(true);
    }

    void ShowPage(string dialouge)
    {
        remain = dialouge ?? "";
        TypingCoroutine = StartCoroutine(StartType());
    }

    void GoNextPage()
    {
        if (!isTyping && remain.Length > 0)
            TypingCoroutine = StartCoroutine(StartType());
    }

    void TypeImmediately()
    {
        StopType();
        while (!string.IsNullOrEmpty(remain))
        {
            string s = GetNextChar();
            if (s != "<end>")
            {
                chatTMP.text += s;
                chatTMP.ForceMeshUpdate();

                if (chatTMP.textInfo.lineCount > lineSize)
                {
                    if (prevChar != " ")
                        remain = prevChar + remain;
                    chatTMP.text = chatTMP.text.Remove(chatTMP.text.Length - 1, 1);
                    StopType();
                    break;
                }
            }
            else
                break;
        }

        if (string.IsNullOrEmpty(remain))
        {
            StopType();
            chatAction?.Invoke();
        }
    }

    IEnumerator StartType()
    {
        isTyping = true;
        chatTMP.text = "";
        while (!string.IsNullOrEmpty(remain))
        {
            string s = GetNextChar();
            if (s != "<end>")
            {
                chatTMP.text += s;
                chatTMP.ForceMeshUpdate();

                if (chatTMP.textInfo.lineCount > lineSize)
                {
                    if (prevChar != " ")
                        remain = prevChar + remain;
                    chatTMP.text = chatTMP.text.Remove(chatTMP.text.Length - 1, 1);
                    StopType();
                    yield break;
                }
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(typeInterval);
        }

        if (string.IsNullOrEmpty(remain))
        {
            StopType();
            chatAction?.Invoke();
        }
    }

    string GetNextChar()
    {
        if (remain.StartsWith("<page>"))
        {
            remain = remain.Remove(0, 6);
            StopType();
            return "<end>";
        }

        //chatTMP.text = chatTMP.text.Remove(chatTMP.text.Length - 1, 1);

        string s = remain.Substring(0, 1);
        prevChar = s;
        remain = remain.Remove(0, 1);

        return s;
    }

    void StopType()
    {
        if (TypingCoroutine != null)
        {
            StopCoroutine(TypingCoroutine);
            TypingCoroutine = null;
        }
        isTyping = false;
    }

    void EndConversation()
    {
        ReceiveStep.gameObject.SetActive(false);
        gameObject.SetActive(false);
        isChatting = false;
    }

    private void OnDestroy()
    {
        ReceiveStep.onClick.RemoveAllListeners();
        CancelTutorial.onClick.RemoveAllListeners();
    }
}