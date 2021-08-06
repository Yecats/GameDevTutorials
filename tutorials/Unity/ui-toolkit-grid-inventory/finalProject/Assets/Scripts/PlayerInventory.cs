using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

namespace Assets.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<StoredItem> StoredItems = new List<StoredItem>();
        public GameObject ItemPrefab;

        private VisualElement m_Root;
        private List<VisualElement> m_ItemRows = new List<VisualElement>();

        private void Awake()
        {
            m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;
            m_ItemRows = m_Root.Q("Container").Q("Body").Query("Inventory").Children<VisualElement>().ToList();

            foreach (var row in m_ItemRows)
            {
                for (int i = 0; i < 9; i++)
                {
                    row.Add(new Slot($"Slot{i}"));
                }
            }
        }
        private void Start()
        {
            foreach (StoredItem item in StoredItems)
            {
                List<Slot> spotInInventory = FindSpaceInInventory(item.Details.Dimensions.Height, item.Details.Dimensions.Width);

                //no space
                if (spotInInventory == null)
                {
                    continue;
                }

                spotInInventory.ForEach(x => x.SetItemInstanceId(item.InstanceGuid));

                GameObject newItem = Instantiate(ItemPrefab, transform);

                //Get the root UI doc of the prefab
                item.RootVisual = newItem.GetComponent<UIDocument>().rootVisualElement;

                ItemVisual slotVE = new ItemVisual(item.Details);
                item.RootVisual.Add(slotVE);


                Vector2 pos = item.Position == Vector2.zero ? GetInitialPosition() : item.Position;
                slotVE.ConfigureVisuals(item.Details, pos);

            }
        }

        private List<Slot> FindSpaceInInventory(int height, int width)
        {

            
            List<Slot> claimedSlots = new List<Slot>();
            int startIndex = 0;

            //Loop the row
            for (int rowIndex = 0; rowIndex < m_ItemRows.Count; rowIndex++)
            {
                var slots = m_ItemRows[rowIndex].Query<Slot>().ToList();
                bool scanning = true;

                //Scan the row to find if there's consecutive slots based on requested with
                while (scanning)
                {
                    List<Slot> consecutiveFreeSlots = slots.Skip(startIndex).TakeWhile(x => x.StoredItemInstanceId.Equals("")).Take(width).ToList();

                    //The required amount was found
                    //Going to log them and bail on this to check the next row
                    if (consecutiveFreeSlots.Count == width)
                    {
                        startIndex = slots.IndexOf(consecutiveFreeSlots[0]);
                        claimedSlots.AddRange(consecutiveFreeSlots);
                        scanning = false;
                    }
                    //Need to check the next set of consecutive items
                    else
                    {
                        startIndex += width;
                        scanning = startIndex + width >= slots.Count;
                    }
                }

                //Check for "done"
                if (claimedSlots.Count == width * height)
                {
                    Debug.Log("FOUND A SPOT!");
                    return claimedSlots;
                }
            }

            //If this point was hit, then the loop never discovered a spot to place stuff :( 
            Debug.Log("WARNING: No room in the inventory. Item cannot be picekd up");
            return null;
        }
    }

    [Serializable]
    public class StoredItem
    {
        public ItemDefinition Details;
        public string InstanceGuid { get; private set; } = Guid.NewGuid().ToString();
        public Vector2 Position;
        public VisualElement RootVisual;

    }

}
