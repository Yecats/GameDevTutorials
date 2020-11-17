using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{ 
    public class IsNavigationActivityTypeOf : Condition
    {
        private NavigationActivity m_ActivityToCheckFor;

        public IsNavigationActivityTypeOf(NavigationActivity activity) : base($"Is Navigation Activity {activity}?")
        {
            m_ActivityToCheckFor = activity;
        }

        protected override void OnReset() { }

        protected override NodeStatus OnRun()
        {
            if (GameManager.Instance == null || GameManager.Instance.NPC == null)
            {
                StatusReason = "GameManager and/or NPC is null";
                return NodeStatus.Failure;
            }

            StatusReason = $"NPC Activity is {m_ActivityToCheckFor}";

            return GameManager.Instance.NPC.MyActivity == m_ActivityToCheckFor ? NodeStatus.Success : NodeStatus.Failure;
        }
    }
}
