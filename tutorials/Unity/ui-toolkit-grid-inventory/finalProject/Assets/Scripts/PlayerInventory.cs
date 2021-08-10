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
        private VisualElement m_InventoryGrid;
        public Dimensions InventoryDimensions;

        [SerializeField]
        private Vector2 m_StartingInventoryPosition;
        [SerializeField]
        private Dimensions m_SingleSlotDimension;

        private void Awake()
        {
            m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;



        }
        async void Start()
        {
            m_InventoryGrid = m_Root.Q<VisualElement>("Grid");


            //await UniTask.WaitForEndOfFrame();

            if (m_InventoryGrid != null)
            {

                m_SingleSlotDimension.Width = (int)m_InventoryGrid.layout.width / InventoryDimensions.Width;
                m_SingleSlotDimension.Height = (int)m_InventoryGrid.layout.height / InventoryDimensions.Height;

                m_StartingInventoryPosition = m_InventoryGrid.worldBound.position;

            }

            foreach (StoredItem item in StoredItems)
            {

                ItemVisual slotVE = new ItemVisual(item.Details, m_SingleSlotDimension);
                m_InventoryGrid.Add(slotVE); // this needs to happen to do the calculation

                bool spaceInInventory = await CalculatePosition(slotVE);

                if (!spaceInInventory)
                {
                    Debug.Log("No space - Cannot pick up the item");
                    m_InventoryGrid.Remove(slotVE);
                    continue;
                }

                item.RootVisual = slotVE;
                slotVE.style.visibility = Visibility.Visible;

            }
        }


        async Task<bool> CalculatePosition(VisualElement newItem)
        {

            for (int i = 0; i < InventoryDimensions.Height; i++)
            {
                for (int j = 0; j < InventoryDimensions.Width; j++)
                {
                    Vector2 newPos = new Vector2(m_SingleSlotDimension.Width * j + 5, m_SingleSlotDimension.Height * i + 5);

                    newItem.style.top = newPos.y;
                    newItem.style.left = newPos.x;

                   // await UniTask.WaitForEndOfFrame();

                    var overlappingItem = StoredItems.Where(x => x.RootVisual != null).FirstOrDefault(x => x.RootVisual.layout.Overlaps(newItem.layout));

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

    public class WaitForPosition : CustomYieldInstruction
    {
        public bool scanningForPosition = true;
        public bool PositionFound = false;
        private VisualElement m_Element;

        public override bool keepWaiting
        {
            get
            {
                return scanningForPosition;
            }
        }
    }
}
