public class TechNode<Tech>
{
    public Tech Data { get; set; }
    public TechNode<Tech> Parent { get; set; }
    public List<TechNode<Tech>> Children { get; set; }
    
}