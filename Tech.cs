class Tech
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Cost;
    public bool researched { get; set; }

    public Tech(string name, string description, Tech parent, List<Tech> children, List<Item> cost)
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