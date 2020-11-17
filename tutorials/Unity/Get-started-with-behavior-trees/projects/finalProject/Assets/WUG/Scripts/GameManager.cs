using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WUG.BehaviorTreeDemo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public NonPlayerCharacter NPC { get; private set; }
        private List<GameObject> m_Waypoints = new List<GameObject>();
        private List<GameObject> m_Items = new List<GameObject>();

        private void Awake()
        {
            //Singleton pattern. Ensures only one version of this lives in the scene
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            m_Waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();
            m_Items = GameObject.FindGameObjectsWithTag("Item").ToList();

            m_Waypoints = m_Waypoints.Shuffle();

            NPC = FindObjectOfType<NonPlayerCharacter>();
        }

        /// <summary>
        /// Sorts the remaining items by distance to NPC and pops the next one off the list
        /// </summary>
        /// <returns>Closest item</returns>
        public GameObject GetClosestItem()
        {
            return m_Items.OrderBy(x => Vector3.Distance(x.transform.position, NPC.transform.position)).FirstOrDefault();
        }

        /// <summary>
        /// Removes an item from the list and scene
        /// </summary>
        /// <param name="item"></param>
        public void PickupItem(GameObject item)
        {
            m_Items.Remove(item);

            Destroy(item);
        }

        /// <summary>
        /// Finds the next waypoint on the list. This is 'random' due to shuffling on Start.
        /// </summary>
        /// <returns>Next waypoint</returns>
        public GameObject GetNextWayPoint()
        {
            if (m_Waypoints != null && m_Waypoints.Count > 0)
            {
                GameObject nextWayPoint = m_Waypoints[0];
                m_Waypoints.RemoveAt(0);

                return nextWayPoint;
            }

            return null;
        }

    }
}
