using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class NPC
{
    [JsonProperty]
    public Data_NPC Origin { get; private set; } // 원본 데이터
    private float favorability;
    [JsonProperty]
    public float Favorability // 호감도
    {
        get { return favorability; }
        private set
        {
            if (value < 0f)
            {
                favorability = 0;
            }
            else if (value > 100f)
            {
                favorability = 100;
            }
            else
            {
                favorability = value;
            }
        }
    }

    [JsonProperty]
    public bool HasMet { get; private set; } = false; // 면식 여부.
    [JsonProperty]
    public bool isGivingQuest { get; private set; } = false; // 중복 의뢰 발생 막기 위한 변수

    public NPC()
    {

    }

    public NPC(Data_NPC data_NPC)
    {
        this.Origin = data_NPC;
        this.Favorability = Origin.favorability;
    }

    // 매 조우 시 발생.
    public void Meet()
    {
        if (!HasMet)
        {
            HasMet = true;
            EventBus.Publish<NPCFirstMetEvent>(new NPCFirstMetEvent(this));
        }
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
        ChangeFavor(favor);
    }

    // 퀘스트 실패 시 발생
    public void FailQuest(float favor)
    {
        isGivingQuest = false;
        ChangeFavor(favor);
    }

    private void ChangeFavor(float favor)
    {
        Favorability += favor;
        EventBus.Publish<NPCChangeFavorEvent>(new NPCChangeFavorEvent(this));
    }
}