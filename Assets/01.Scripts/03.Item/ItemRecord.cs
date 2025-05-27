using Newtonsoft.Json;

[System.Serializable]
public class ItemRecord
{
    [JsonProperty]
    public bool HasDiscovered { get; private set; }

    public ItemRecord()
    {
    }

    public void RecordDiscover()
    {
        HasDiscovered = true;
    }
}
