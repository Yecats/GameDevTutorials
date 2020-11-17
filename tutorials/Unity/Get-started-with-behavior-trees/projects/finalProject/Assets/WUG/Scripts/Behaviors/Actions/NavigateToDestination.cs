using System;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public class NavigateToDestination : Node
    {
        private Vector3 m_TargetDestination;

        public NavigateToDestination()
        {
            Name = "Navigate";
        }

        protected override void OnReset() { }

        protected override NodeStatus OnRun()
        {
            //Confirm all references exist
            if (GameManager.Instance == null || GameManager.Instance.NPC == null)
            {
                StatusReason = "GameManager or NPC is null";
                return NodeStatus.Failure;
            }

            //Perform logic that should only run once
            if (EvaluationCount == 0)
            {
                //Get destination from Game Manager 
                GameObject destinationGO = GameManager.Instance.NPC.MyActivity == NavigationActivity.PickupItem ? GameManager.Instance.GetClosestItem() : GameManager.Instance.GetNextWayPoint();

                //Confirm that the destination is valid - If not, fail.
                if (destinationGO == null)
                {
                    StatusReason = $"Unable to find game object for {GameManager.Instance.NPC.MyActivity}";
                    return NodeStatus.Failure;
                }

                //Get a valid location on the NavMesh that's near the target destination
                NavMesh.SamplePosition(destinationGO.transform.position, out NavMeshHit hit, 1f, 1);

                //Set the location for checks later
                m_TargetDestination = hit.position;

                //Set the destination on the NavMesh. This tells the AI to start moving to the new location.
                GameManager.Instance.NPC.MyNavMesh.SetDestination(m_TargetDestination);
                StatusReason = $"Starting to navigate to {destinationGO.transform.position}";

                //Return running, as we want to continue to have this node evaluate
                return NodeStatus.Running;
            }

            //Calculate how far the AI is from the destination
            float distanceToTarget = Vector3.Distance(m_TargetDestination, GameManager.Instance.NPC.transform.position);

            //If the AI is within .25f then navigation will be considered a success
            if (distanceToTarget < .25f)
            {
                StatusReason = $"Navigation ended. " +
                    $"\n - Evaluation Count: {EvaluationCount}. " +
                    $"\n - Target Destination: {m_TargetDestination}" +
                    $"\n - Distance to target: {Math.Round(distanceToTarget, 1)}";

                return NodeStatus.Success;
            }

            //Otherwise, the AI is still on the move
            StatusReason = $"Distance to target: {distanceToTarget}";
            return NodeStatus.Running;

        }
    }
}
