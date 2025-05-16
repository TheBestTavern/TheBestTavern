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

    protected  async virtual void Start()
    {
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);

    }

    public virtual void OnEnable()
    {

    }


    /// <summary>
    /// 다른 곳에서 사용할 함수
    /// </summary>

    //public virtual void OpenLetter(Quest quest, QuestBaseSlot slot)
    //{
    //    //2. 초기화
    //    currentLetter.FirstInit(quest, RemoveSlot);
    //}

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