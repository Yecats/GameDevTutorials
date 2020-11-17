using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public abstract class Node : NodeBase
    {
        public int EvaluationCount;
        private string m_LastStatusReason { get; set; } = "";

        public virtual NodeStatus Run()
        {
            NodeStatus nodeStatus = OnRun();

            if (LastNodeStatus != nodeStatus || !m_LastStatusReason.Equals(StatusReason))
            {
                LastNodeStatus = nodeStatus;
                m_LastStatusReason = StatusReason;
                OnNodeStatusChanged(this);
            }

            EvaluationCount++;

            if (nodeStatus != NodeStatus.Running)
            {
                Reset();
            }

            return nodeStatus;
        }

        public void Reset()
        {
            EvaluationCount = 0;
            OnReset();
        }

        protected abstract NodeStatus OnRun();
        protected abstract void OnReset();
    }
}
