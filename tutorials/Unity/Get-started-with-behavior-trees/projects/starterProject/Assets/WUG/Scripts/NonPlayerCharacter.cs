using UnityEngine;
using UnityEngine.AI;

namespace WUG.BehaviorTreeDemo
{
    public enum NavigationActivity
    {
        Waypoint, 
        PickupItem
    }

    public class NonPlayerCharacter : MonoBehaviour
    {
        public NavMeshAgent MyNavMesh { get; private set; }
        public NavigationActivity MyActivity { get; set; }


        private void Start()
        {
            MyNavMesh = GetComponent<NavMeshAgent>();
            MyActivity = NavigationActivity.Waypoint;

        }
                
    }
}
