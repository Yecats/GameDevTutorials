using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace WUG.BehaviorTreeDemo
{
    public class Timer : Decorator
    {
        private float m_StartTime;
        private bool m_UseFixedTime;
        private float m_TimeToWait;

        /// <summary>
        /// Run the child node a specified amount of time in seconds before exiting
        /// </summary>
        /// <param name="name">Friendly name - displayed on the Behavior Tool Debugger UI if set</param>
        /// <param name="childNode">Node to run</param>
        /// <param name="timeToWait">Amount of time to wait in seconds</param>
        /// <param name="useFixedTime">Whether to use Time.fixedTime (true) or Time.time (false)</param>
        public Timer(float timeToWait, Node childNode, bool useFixedTime = false) : base($"Timer for {timeToWait}", childNode)
        {
            m_UseFixedTime = useFixedTime;
            m_TimeToWait = timeToWait;
        }

        protected override void OnReset() { }
        protected override NodeStatus OnRun()
        {
            //Confirm that a valid child node was passed in the constructor
            if (ChildNodes.Count == 0 || ChildNodes[0] == null)
            {
                return NodeStatus.Failure;
            }

            // Run the child node and calculate the elapsed
            NodeStatus originalStatus = (ChildNodes[0] as Node).Run();

            //If this is the first eval, then the start time needs to be set up.
            if (EvaluationCount == 0)
            {
                StatusReason = $"Starting timer for {m_TimeToWait}. Child node status is: {originalStatus}";
                m_StartTime = m_UseFixedTime ? Time.fixedTime : Time.time;
            }

            //Calculate how much time has passed
            float elapsedTime = Time.fixedTime - m_StartTime;

            //If more time has passed than we wanted, it's time to stop
            if (elapsedTime > m_TimeToWait)
            {
                StatusReason = $"Timer complete - Child node status is: { originalStatus}";
                return NodeStatus.Success;
            }

            //Otherwise, keep running
            StatusReason = $"Timer is {elapsedTime} out of {m_TimeToWait}. Child node status is: {originalStatus}";
            return NodeStatus.Running;

        }
    }
}
