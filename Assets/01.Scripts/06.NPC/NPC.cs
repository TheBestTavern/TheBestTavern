using System;

public class NPC
{
    public Data_NPC origin { get; private set; } // 원본 데이터
    private float favorability;
    public float Favorability // 호감도
    {
        get { return favorability; }
        private set
        {
            if(value < 0f)
            {
                favorability = 0;   
            }
            else if(value > 100f)
            {
                favorability = 100;
            }
            else
            {
                favorability = value;
            }
        }
    } 

    public bool HasMet { get; private set; } = false; // 면식 여부.

    public bool isGivingQuest { get; private set; } = false; // 중복 의뢰 발생 막기 위한 변수

    public NPC(Data_NPC data_NPC)
    {
        this.origin = data_NPC;
        this.Favorability = origin.favorability;
    }

    // 처음 조우 시 발생.
    public void Meet()
    {
        HasMet = true;
    }

    // 퀘스트를 의뢰함에 접수 시 발생.
    public void GiveQuest()
    {
        isGivingQuest = true;
    }

    // 퀘스트를 성공 시 발생.
    public void SuccessQuest(float favor)
    {
        isGivingQuest = false;
        Favorability += favor;
    }

    // 퀘스트 실패 시 발생
    public void FailQuest()
    {
        isGivingQuest = false;
    }
}