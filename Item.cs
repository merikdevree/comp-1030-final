public class Item
{
    public string Type { get; set; }
    public int Amount { get; set; }

    public Item(string type, int amount)
    {
        Type = type;
        Amount = amount;
    }
}