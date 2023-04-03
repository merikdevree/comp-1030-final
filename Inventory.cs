class Inventory
{
    public List<Item> InventoryItems { get; set; }

    public Inventory()
    {
    InventoryItems = new List<Item>();
    }

    // check if item type is already in inventory
    // if so, add to amount
    // if not, add to inventory
    void AddItem(Item item)
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
    void RemoveItem(Item item)
    {
    InventoryItems.Remove(item);
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
        return null;
    }
}