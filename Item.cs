public class Item
{
    public string Type;
    public int Amount;
    public Item[] requiredItems;
    public Item(string type, int amount, Item[] required = null)
    {
        Type = type;
        Amount = amount;
        requiredItems = required;
    }
    
}