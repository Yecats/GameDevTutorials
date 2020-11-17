using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public class Selector : Composite
    {
        public Selector(string displayName, params Node[] childNodes) : base(displayName, childNodes) { }

        protected override NodeStatus OnRun()
        {
            if (CurrentChildIndex >= ChildNodes.Count)
            {
                return NodeStatus.Failure;
            }

            NodeStatus nodeStatus = (ChildNodes[CurrentChildIndex] as Node).Run();

            switch (nodeStatus)
            {
                case NodeStatus.Failure:
                    CurrentChildIndex++;
                    break;
                case NodeStatus.Success:
                    return NodeStatus.Success;
            }

            //in progress
            return NodeStatus.Running;
        }

        protected override void OnReset()
        {
            CurrentChildIndex = 0;

            for (int i = 0; i < ChildNodes.Count; i++)
            {
                (ChildNodes[i] as Node).Reset();
            }
        }
    }
}
