using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public abstract class MailBoxContentBase : MonoBehaviour
{
    public MailBoxContentType ContentType;

    [SerializeField] protected QuestBaseSlot slotPref;
    [SerializeField] protected Transform slotPrt;
    protected List<QuestBaseSlot> slots = new();

    protected QuestBaseLetter currentLetter;

    protected bool isReadyTodaySlot;

    private void Start()
    {
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    public virtual void OnEnable()
    {
        
    }

    public virtual void MakeSlot(List<int> quests)
    {
        //if (isReady) return;

        // 1. slot 생성(퀘스트 리스트로 slot생성) (나중에 슬롯을 pool로 관리하면 좋을듯)
        QuestBaseSlot pref;
        int i = 1;
        foreach (var questID in quests)
        {
            pref = Instantiate(slotPref, slotPrt);
            pref.Init(this);
            pref.SetSlot(questID, i);
            slots.Add(pref);
            i++;
        }

        // 2. isReady true로 바꾸기.
        //isReady = true;
    }


    /// <summary>
    /// 다른 곳에서 사용할 함수
    /// </summary>

    public virtual void OpenLetter(Quest quest, QuestBaseSlot slot)
    {
        //2. 초기화
        currentLetter.FirstInit(quest, RemoveSlot);

        // 3. 편지 내용 채우기
        currentLetter.EveryInit(quest, slot);
    }

    public void RemoveSlot(QuestBaseSlot slot)
    {
        slots.Remove(slot);
        Destroy(slot.gameObject);
    }

    public class OnNewDay : IDayCommand
    {
        public MailBoxContentBase prt;
        public OnNewDay(MailBoxContentBase mailBox)
        {
            this.prt = mailBox;
        }

        public int Priority => 1800;

        public Task Execute()
        {
            prt.isReadyTodaySlot = false;

            return Task.CompletedTask;
        }

        public bool isValid()
        {
            return prt != null;
        }
    }
}