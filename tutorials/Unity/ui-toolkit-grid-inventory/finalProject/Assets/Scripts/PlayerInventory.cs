using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections;

namespace Assets.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<StoredItem> StoredItems = new List<StoredItem>();
        public GameObject ItemPrefab;

        private VisualElement m_Root;
        public Dimensions InventoryDimensions;

        [SerializeField]
        private Vector2 m_StartingInventoryPosition;
        [SerializeField]
        private Dimensions m_SingleSlotDimension = new Dimensions();

        private void Awake()
        {
            m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;



        }
        IEnumerator Start()
        {
            VisualElement inventoryGrid = m_Root.Q<VisualElement>("Grid");
            yield return new WaitForSeconds(.1f);

            if (inventoryGrid != null)
            {

                m_SingleSlotDimension.Width = (int)inventoryGrid.layout.width / InventoryDimensions.Width;
                m_SingleSlotDimension.Height = (int)inventoryGrid.layout.height / InventoryDimensions.Height;

                m_StartingInventoryPosition = inventoryGrid.worldBound.position;

            }

            foreach (StoredItem item in StoredItems)
            {

                GameObject newItem = Instantiate(ItemPrefab, transform);
                VisualElement newItemRoot = newItem.GetComponent<UIDocument>().rootVisualElement;

                ItemVisual slotVE = new ItemVisual(item.Details);
                newItemRoot.Add(slotVE); // this needs to happen to do the calculation

                yield return new WaitForSeconds(.1f);

                slotVE.SetSize(m_SingleSlotDimension.Width * item.Details.SlotDimension.Width - 10, m_SingleSlotDimension.Height * item.Details.SlotDimension.Height - 10);
                bool spaceInInventory = CalculatePosition(slotVE);

                if (!spaceInInventory)
                {
                    Debug.Log("No space - Cannot pick up the item");
                    Destroy(newItem);
                    continue;
                }

                item.RootVisual = newItemRoot;

            }
        }

        private bool CalculatePosition(VisualElement newItem)
        {

            for (int i = 1; i <= InventoryDimensions.Height; i++)
            {
                for (int j = 1; j <= InventoryDimensions.Width; j++)
                {
                    Vector2 newPos = new Vector2(m_StartingInventoryPosition.x * j, m_StartingInventoryPosition.y * i);

                    newItem.parent.style.top = newPos.y;
                    newItem.parent.style.left = newPos.x;
                    var overlappingItem = StoredItems.Where(x => x.RootVisual != null).FirstOrDefault(x => x.RootVisual.contentRect.Overlaps(newItem.contentRect));

                    //Nothing is here! Place the item.
                    if (overlappingItem == null)
                    {
                        return true;
                    }
                }
            }

            return false;
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
