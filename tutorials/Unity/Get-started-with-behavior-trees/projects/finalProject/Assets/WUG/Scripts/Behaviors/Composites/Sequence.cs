using System.Linq;
using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public class Sequence : Composite
    {
        public Sequence(string displayName, params Node[] childNodes) : base(displayName, childNodes) { }

        protected override void OnReset()
        {
            CurrentChildIndex = 0;

            for (int i = 0; i < ChildNodes.Count; i++)
            {
                (ChildNodes[i] as Node).Reset();
            }
        }

        protected override NodeStatus OnRun()
        {
            //Check the status of the last child
            NodeStatus childNodeStatus = (ChildNodes[CurrentChildIndex] as Node).Run();

            //Evaluate the current child node. If it's failed - sequence should fail. 
            switch (childNodeStatus)
            {
                //Child failed - return failure
                case NodeStatus.Failure:
                    return childNodeStatus;
                //It succeeded - move to the next child
                case NodeStatus.Success:
                    CurrentChildIndex++;
                    break;
            }

            //All children have run successfully - return success
            if (CurrentChildIndex >= ChildNodes.Count)
            {
                return NodeStatus.Success;
            }

            //The child was a success but we still have more to do - so call this method again.
            return childNodeStatus == NodeStatus.Success ? OnRun() : NodeStatus.Running;
        }
    }
}
