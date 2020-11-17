using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public class AreItemsNearBy : Condition
    {
        private float m_DistanceToCheck;

        public AreItemsNearBy(float maxDistance) : base($"Are Items within {maxDistance}f?")
        {
            m_DistanceToCheck = maxDistance;
        }

        protected override void OnReset() { }

        protected override NodeStatus OnRun()
        {
            //Check for references
            if (GameManager.Instance == null || GameManager.Instance.NPC == null)
            {
                StatusReason = "GameManager and/or NPC is null";
                return NodeStatus.Failure;
            }

            //Get the closest item
            GameObject item = GameManager.Instance.GetClosestItem();

            //Check to see if something is close by
            if (item == null)
            {
                StatusReason = "No items near by";
                return NodeStatus.Failure;

            }
            else if (Vector3.Distance(item.transform.position, GameManager.Instance.NPC.transform.position) > m_DistanceToCheck)
            {
                StatusReason = $"No items within range of {m_DistanceToCheck} meters";
                return NodeStatus.Failure;
            }

            return NodeStatus.Success;
        }
    }
}
