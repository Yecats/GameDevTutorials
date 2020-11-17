using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public class SetNavigationActivityTo : Node
    {

        private NavigationActivity m_NewActivity;

        public SetNavigationActivityTo(NavigationActivity newActivity)
        {
            m_NewActivity = newActivity;
            Name = $"Set NavigationActivity to {m_NewActivity}";
        }
        protected override void OnReset() { }

        protected override NodeStatus OnRun()
        {
            if (GameManager.Instance == null || GameManager.Instance.NPC == null)
            {
                StatusReason = "GameManager and/or NPC is null";
                return NodeStatus.Failure;
            }

            GameManager.Instance.NPC.MyActivity = m_NewActivity;

            return NodeStatus.Success;
        }
    }
}
