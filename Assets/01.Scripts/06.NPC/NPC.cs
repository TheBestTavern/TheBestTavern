public class NPC
{
    public Data_NPC origin {  get; private set; } // 원본 데이터
    public float favorability { get; private set; } // 호감도
    public bool HasMet { get; private set; } = false; // 면식 여부.
    public bool isGivingQuest { get; private set; } = false; // 중복 의뢰 발생 막기 위한 변수
    
    public NPC(Data_NPC data_NPC)
    {
        this.origin = data_NPC;
        this.favorability = origin.favorability; 
    }

    // 처음 조우시 발생.
    public void Meet()
    {
        HasMet = true;
    }

    // 퀘스트를 의뢰함에 접수시 발생.
    public void GiveQuest()
    {
        isGivingQuest = true;
    }

    // 퀘스트를 완료시 발생.
    public void CompleteQuest()
    {
        isGivingQuest = false;
    }
}