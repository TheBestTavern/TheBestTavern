using Unity.VisualScripting;

public class Quest
{
    public int Id { get; private set; }
    public bool isAccepted { get; private set; }
    public int AcceptedDay { get; private set; }
    public int TriggerDat { get; private set; }
    public string Title{ get; private set; }
    public string BodyText { get; private set; }
    public string NPCname { get; private set; }

    public Quest(int id, string title, string bodyText, string nPCname)
    {
        Id = id;
        Title = title;
        BodyText = bodyText;
        NPCname = nPCname;
    }
}