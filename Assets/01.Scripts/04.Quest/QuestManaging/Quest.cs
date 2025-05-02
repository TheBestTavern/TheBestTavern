using System;
using System.Globalization;
using Unity.VisualScripting;

public class Quest
{
    public Data_Quest origin { get; private set; } // 원본 데이터

    public bool IsCompletedOnce { get; private set; } = false; // 한번이라도 클리어 된 적 있는지.

    public bool IsAccepted { get; private set; } = false; // 퀘스트 수락 여부
    public LunarDateTime? AcceptedDate { get; private set; } // 퀘스트 수락일
    public LunarDateTime? TriggerDate { get; private set; } // npc가 찾아올 날

    public bool? IsSuccessful { get; private set; } // 퀘스트 성공 여부
    public LunarDateTime? RecycleDate { get; private set; } // 다시 퀘스트가 출몰할 날
    public bool RecycleDatePass { get; private set; } = true; // 재활용 주기 지났는지
    int RecycleDays = 5; // 재활용에 필요한 일수. 임시로 5일로 지정

    public Quest(Data_Quest data_Quest)
    {
        this.origin = data_Quest;
    }

    public void AcceptQuest(LunarDateTime todayDateTime, int afterDays)
    {
        IsAccepted = true;
        AcceptedDate = todayDateTime;
        TriggerDate = todayDateTime.AddDays(afterDays);
        RecycleDatePass = false;
        IsSuccessful = null;
    }

    public void CompleteQuest(LunarDateTime todayDateTime) // 퀘스트 성공
    {
        if(!IsCompletedOnce) IsCompletedOnce = true;
        IsAccepted = false;
        AcceptedDate = null;
        TriggerDate = null;
        RecycleDate = todayDateTime.AddDays(RecycleDays); // 퀘스트 완료 시 다음 재출현일자 미리 지정.
        IsSuccessful = true;
    }

    public void FailQuest(LunarDateTime todayDateTime) // 퀘스트 실패
    {
        IsAccepted = false;
        AcceptedDate = null;
        TriggerDate = null;
        RecycleDate = todayDateTime.AddDays(RecycleDays); // 퀘스트 완료 시 다음 재출현일자 미리 지정.
        IsSuccessful = false;
    }

    // 매일 퀘스트 출현 가능한지 체크.
    // 퀘스트 부여 가능(완료 날짜로부터 지났는지, 타겟 npc의 호감도 조건 이상이어야함) // npc가 다른 퀘스트를 주고 있지 않은지는 조건으로 쓸지 말지 고민중. 조건으로 쓰면 로직이 좀 복잡해짐
    public bool CheckAvailable()
    {
        LunarDateTime today = TimerManager.Instance.GetToday();
        // 받고 있는 퀘스트인지 체크
        if (IsAccepted)
        {
            return false;
        }

        // 재활용 주기 지난지 체크
        if (!RecycleDatePass && !CheckRecycleDate(today))
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

    private bool CheckRecycleDate(LunarDateTime todayDateTime)
    {
        if (todayDateTime >= RecycleDate)
        {
            RecycleDatePass = true;
            RecycleDate = null;
            return true;
        }
        else
        {
            return false;
        }
    }
}