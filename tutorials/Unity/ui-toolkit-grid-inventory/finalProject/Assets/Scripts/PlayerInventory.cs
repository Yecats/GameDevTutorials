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
                    row.Add(new Slot("", $"Slot{i}"));
                }
            }
        }
        private void Start()
        {
            foreach (StoredItem item in StoredItems)
            {
                GameObject newItem = Instantiate(ItemPrefab, transform);
                
                //Get the root UI doc of the prefab
                item.RootVisual = newItem.GetComponent<UIDocument>().rootVisualElement;
                
                ItemVisual slotVE = new ItemVisual(item.Details);
                item.RootVisual.Add(slotVE);
                
                Vector2 pos = item.Position == Vector2.zero ? GetInitialPosition(item.Details.Dimensions.Height, item.Details.Dimensions.Width) : item.Position;
                slotVE.ConfigureVisuals(item.Details, pos);

            }
        }

        //Loop each row
            //Scan each rows children to until an empty one is found
            //Check surrounding area based on height / width dimensions to see if the required space is open
            //if Yes - place
                //if no, restart - looking at all children. 
                    // If reached end of row's children, scan next row
                    //If reach end, do not pick up (debug log)

        private Vector2 GetInitialPosition(int height, int width)
        {
            Vector2 position = Vector2.zero;

            List<Slot> claimedSlots = new List<Slot>();

            for (int rowIndex = 0; rowIndex < m_ItemRows.Count; rowIndex++)
            {
                //Check to see if the right amount of slots were detected
                if (claimedSlots.Count == height + width)
                {
                    break;
                }

                var slots = m_ItemRows[rowIndex].Query<Slot>().ToList();
                bool notEnoughSpace = false;

                for (int i = 0; i < slots.Count; i++)
                {


                    claimedSlots.Clear();
                    notEnoughSpace = false;

                    if (slots[i].StoredItemInstanceId.Equals(""))
                    {
                        claimedSlots.Add(slots[i]);

                        //making sure there's enough width
                        int remaining = slots.Count - i - 1;
                        if (remaining >= width)
                        {
                            for (int j = i + 1; j < i + width; j++)
                            {
                                if (!slots[j].StoredItemInstanceId.Equals(""))
                                {
                                    notEnoughSpace = true;
                                    break;
                                }

                                claimedSlots.Add(slots[j]);
                            }

                            //Dope.. starting over.
                            if (notEnoughSpace)
                            {
                                continue;
                            }

                            //Need to make sure there's enough rows for the height... if not no point in the loop
                            remaining = m_ItemRows.Count - i;

                            if (remaining >= height)
                            {
                                for (int j = rowIndex + 1; j < height; j++)
                                {
                                    var slot = m_ItemRows[rowIndex + j].Children().Skip(rowIndex + j - 1).First().Q<Slot>();

                                    if (!slot.StoredItemInstanceId.Equals(""))
                                    {
                                        notEnoughSpace = true;
                                        break;
                                    }

                                    claimedSlots.Add(slots[j]);
                                }
                            }

                            //Dope.. starting over.
                            if (!notEnoughSpace)
                            {
                                break;
                            }
                        }
                    }
                }

                //Hit the end and there was not room in the inventory for this!
                if (notEnoughSpace && rowIndex == m_ItemRows.Count - 1)
                {
                    Debug.Log("WARNING: No room in the inventory. Item cannot be picekd up");
                }

                //found something
                else if (!notEnoughSpace)
                {
                    break;
                }
                //still scanning...
            }

            return position;
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
