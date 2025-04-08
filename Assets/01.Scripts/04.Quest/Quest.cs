using Unity.VisualScripting;

public class Quest
{
    public int Id { get; private set; }
    public bool isAccepted { get; private set; }
    public int AcceptedDay { get; private set; }
    public int TriggerDat { get; private set; }

    public void Init(int id, int day)
    {

    }
}