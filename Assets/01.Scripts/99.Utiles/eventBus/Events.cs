
// Menu - npc 관계도 이벤트
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
    public QuestClickLetterBtnEvent(Quest quest)
    {
        this.quest = quest;
    }
}