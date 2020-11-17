using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

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
