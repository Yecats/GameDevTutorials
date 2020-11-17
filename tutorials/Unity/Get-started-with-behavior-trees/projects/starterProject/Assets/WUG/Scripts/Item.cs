using UnityEngine;

namespace WUG.BehaviorTreeDemo
{
    public class Item : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (GameManager.Instance.NPC.MyActivity == NavigationActivity.PickupItem)
            {
                GameManager.Instance.PickupItem(this.gameObject);
            }
        }
    }
}
