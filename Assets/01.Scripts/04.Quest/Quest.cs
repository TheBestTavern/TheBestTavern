using System;
using Unity.VisualScripting;

public class Quest
{
    public Data_Quest origin { get; private set; } // 원본 데이터

    public bool IsCompletedOnce { get; private set; } = false; // 한번이라도 클리어 된 적 있는지.

    public bool IsAccepted { get; private set; } = false; // 퀘스트 수락 여부
    public DateTime AcceptedDate { get; private set; } // 퀘스트 수락일
    public DateTime TriggerDate { get; private set; } // npc가 찾아올 날


    public DateTime RecycleDate { get; private set; } // 다시 퀘스트가 출몰할 날
    public bool RecycleDatePass { get; private set; } = false; // 재활용 주기 지났는지
    int RecycleDays = 5; // 재활용에 필요한 일수

    //public int Id { get; private set; } // key로 대체
    //public string Title{ get; private set; }// name로 대체
    //public string BodyText { get; private set; }// description로 대체
    //public string NPCname { get; private set; } // givingNPC로 대체


    public Quest(Data_Quest data_Quest)
    {
        this.origin = data_Quest;
    }

    public void AcceptQuest(DateTime todayDateTime, int afterDays)
    {
        IsAccepted = true;
        AcceptedDate = todayDateTime;
        TriggerDate = todayDateTime.AddDays(afterDays);
        RecycleDatePass = false;
    }

    public void CompleteQuest(DateTime todayDateTime)
    {
        IsCompletedOnce = true;
        IsAccepted = false;
        AcceptedDate = new DateTime();
        TriggerDate = new DateTime();
        RecycleDate = todayDateTime.AddDays(RecycleDays); // 퀘스트 완료 시 다음 재출현일자 미리 지정.
    }

    // 매일 퀘스트 출현 가능한지 체크.
    // 퀘스트 부여 가능(완료 날짜로부터 지났는지, 타겟 npc의 호감도 조건 이상이어야함) // npc가 다른 퀘스트를 주고 있지 않은지는 조건으로 쓸지 말지 고민중. 조건으로 쓰면 로직이 좀 복잡해짐
    public bool CheckAvailable(DateTime todayDateTime)
    {
        // 받고 있는 퀘스트인지 체크
        if (IsAccepted)
        {
            return false;
        }

        // 재활용 주기 지난지 체크
        if (!RecycleDatePass && !CheckRecycleDate(todayDateTime))
        {
            return false;
        }

        //NPC 호감도 체크.
        // 1.타겟 npc(origin.givingNPC), 2.npc목록에서 조회, 3.퀘스트의 조건 호감도와 비교
        if (NPCManager.Instance.NPCData.AllNPC[origin.givingNPC].favorability >= origin.conditionFavorability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckRecycleDate(DateTime todayDateTime)
    {
        if (todayDateTime >= RecycleDate)
        {
            RecycleDatePass = true;
            RecycleDate = new DateTime();
            return true;
        }
        else
        {
            return false;
        }
    }
}