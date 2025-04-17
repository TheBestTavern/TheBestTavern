
public class Item
{
    public Data_Foods origin {  get; private set; }
    public bool HasHad {  get; private set; }
    public bool IsHaving {  get; private set; }

    public Item(Data_Foods origin)
    {
        this.origin = origin;
    }
}