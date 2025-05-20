
// Menu - npc 관계도 이벤트
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

public class NPCSuccessQuestEvent
{
    public NPC npc;
    public NPCSuccessQuestEvent(NPC npc)
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

    public ItemStackOnZeroEvent(int iD)
    {
        ID = iD;
    }
}
public class ItemStackOnChangeEvent
{
    public int ID;
    public ItemStackOnChangeEvent(int iD)
    {
        ID = iD;
    }
}

// 툴팁 관련 이벤트

public class SlotHoverEnterEvent
{
    public int ID;
    public  SlotHoverEnterEvent(int id)
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