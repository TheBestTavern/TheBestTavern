
// Menu - npc 관계도 이벤트
using Unity.VisualScripting;
using UnityEngine;

public class NPCFirstMetEvent
{
    public NPC npc;
    public NPCFirstMetEvent(NPC npc)
    {
        this.npc = npc;
    }
}

public class NPCChangeFavorEvent
{
    public NPC npc;
    public NPCChangeFavorEvent(NPC npc)
    {
        this.npc = npc;
    }
}

public class NPCGetQuestRewardEvent
{
    public NPC npc;
    public NPCGetQuestRewardEvent(NPC npc)
    {
        this.npc = npc;
    }
}

// Menu - quest 이벤트
public class QuestSuccessFirstEvent
{
    public QuestSuccessFirstEvent()
    {
    }
}

public class QuestAcceptEvent
{
    public QuestAcceptEvent()
    {
    }
}

public class QuestCompleteEvent
{
    public QuestCompleteEvent()
    {
    }
}

public class QuestClickLetterBtnEvent
{
    public Quest quest;
    public bool isInProgressQuest;
    public QuestClickLetterBtnEvent(Quest quest, bool isInProgressQuest)
    {
        this.quest = quest;
        this.isInProgressQuest = isInProgressQuest;
    }
}

// 밤 동안 메인씬 UI 클릭 안되도록.
public class EnterNightUIBlockEvent
{

    public EnterNightUIBlockEvent()
    {

    }
}

public class EndNightUIBlockEvent
{

    public EndNightUIBlockEvent()
    {

    }
}

// 아이템 스택 관련 이벤트
public class ItemStackOnZeroEvent
{
    public int ID;
    public InvenType invenType;

    public ItemStackOnZeroEvent(int iD, InvenType invenType)
    {
        ID = iD;
        this.invenType = invenType;
    }
}
public class ItemStackOnChangeEvent
{
    public int ID;
    public InvenType invenType;
    public ItemStackOnChangeEvent(int iD, InvenType invenType)
    {
        ID = iD;
        this.invenType = invenType;
    }
}

// 툴팁 관련 이벤트

public class SlotHoverEnterEvent
{
    public int ID;
    public SlotHoverEnterEvent(int id)
    {
        ID = id;
    }
}

public class SlotHoverEndEvent
{
    public SlotHoverEndEvent()
    {
    }
}

// 계절 변경 관련 

public class SeasonChangeEvent
{
    public DesignEnums.SeasonType season;

    public SeasonChangeEvent(DesignEnums.SeasonType season)
    {
        this.season = season;
    }
}

// NPC 방문 이벤트

public class NPCVisitEvent
{
    public NPCVisitEvent()
    {
    }
}


// 튜토리얼 관련 이벤트


public class OpenPopup
{
    public PopUpType popUpType;
    public string detail;
    public OpenPopup(PopUpType popUpType, string detail) => (this.popUpType, this.detail) = (popUpType, detail);
}
public class GainItem { }
public class SceneMove
{
    public string SceneName;
    public SceneMove(string SceneName) => this.SceneName = SceneName;
}
public class EnterSubmissionMode
{
    public int npcID;
    public EnterSubmissionMode(int npcID) => this.npcID = npcID;
}
public class AcceptQuest
{
    public int questID;
    public AcceptQuest(int questID) => this.questID = questID;
}
public class OpenProgressInLetter
{
    public int questID;
    public OpenProgressInLetter(int questID) => this.questID = questID;
}
