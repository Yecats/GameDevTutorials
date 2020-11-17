namespace WUG.BehaviorTreeDemo
{
    public abstract class Decorator : Node
    {
        public Decorator(string displayName, Node node)
        {
            Name = displayName;
            ChildNodes.Add(node);
        }
    }
}
