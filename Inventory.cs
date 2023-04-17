public class Inventory
{
    public List<Item> InventoryItems { get; set; }

    public Inventory()
    {
    InventoryItems = new List<Item>();
    }

    // check if item type is already in inventory
    // if so, add to amount
    // if not, add to inventory
    public void AddItem(Item item)
    {
        if (item.Type == Search(item.Type).Type)
        {
            Search(item.Type).Amount += item.Amount;
        }
        else
        {
        InventoryItems.Add(item);
        }
    }
    public void RemoveItem(string type, int amount)
    {
        if (Search(type).Amount == 0)
        {
            InventoryItems.Remove(Search(type));
        }
        else
        {
            Search(type).Amount -= amount;
        }
    }
    // search inventory by item type
    // return item if found
    public Item Search(string type)
    {
        foreach (Item item in InventoryItems)
        {
            if (item.Type == type)
            {
                return item;
            }
        }
        return new Item("null", 0);
    }

    public bool HasItem(string type, int amount = 1)
    {
        foreach (Item item in InventoryItems)
        {
            if (item.Type == type && item.Amount >= amount)
            {
                return true;
            }
        }
        return false;
    }
    
}