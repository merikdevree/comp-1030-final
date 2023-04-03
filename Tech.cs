class Tech
{
    public string Name { get; set; }
    public string Description { get; set; }
   
    public readonly List<KeyValuePair<Item, int>> Cost;
    public bool researched { get; set; }

    public Tech(string name, string description, Tech parent, List<Tech> children, List<KeyValuePair<Item, int>> cost)
    {
        Name = name;
        Description = description;
        Cost = cost;
        researched = false;
    }

    public void Research()
    {
        researched = true;
    }

}