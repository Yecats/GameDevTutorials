using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance;

        public List<StoredItem> StoredItems = new List<StoredItem>();
        public GameObject ItemPrefab;

        private VisualElement m_Root;
        private VisualElement m_InventoryGrid;
        public Dimensions InventoryDimensions;

        [SerializeField]
        private Vector2 m_StartingInventoryPosition;

        [SerializeField]
        private Dimensions m_SingleSlotDimension;

        private Label m_ItemDetailHeader;
        private Label m_ItemDetailBody;
        private Label m_ItemDetailPrice;
        private VisualElement m_GhostRect;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;

            }
            else if (Instance != this)
            {
                Debug.LogError("Two versions of PlayerInventory detected. Destroying GO");
                Destroy(this);
            }
        }
        async void Start()
        {
            m_InventoryGrid = m_Root.Q<VisualElement>("Grid");
            m_ItemDetailHeader = m_Root.Q<VisualElement>("ItemDetails").Q<Label>("Header");
            m_ItemDetailBody = m_Root.Q<VisualElement>("ItemDetails").Q<Label>("Body");
            m_ItemDetailPrice = m_Root.Q<VisualElement>("ItemDetails").Q<Label>("SellPrice");

            //Create the ghost rect
            m_GhostRect = new VisualElement();
            m_GhostRect.style.position = Position.Absolute;
            m_GhostRect.AddToClassList("slot-icon-highlighted");
            m_GhostRect.style.visibility = Visibility.Hidden;
            m_GhostRect.name = "Ghoest Rect";
            m_InventoryGrid.Add(m_GhostRect);

            await UniTask.WaitForEndOfFrame();

            if (m_InventoryGrid != null && m_InventoryGrid.childCount > 0)
            {
                VisualElement firstSlot = m_InventoryGrid.Children().First();

                m_SingleSlotDimension.Width = firstSlot.worldBound.width;
                m_SingleSlotDimension.Height = firstSlot.worldBound.height;

                m_StartingInventoryPosition = firstSlot.worldBound.position;

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


        internal (bool canPlace, Vector2 position) ShowPlacementTarget(ItemVisual draggedItem)
        {
            VisualElement targetSlot = m_InventoryGrid.Children().Where(x => x.layout.Overlaps(draggedItem.layout) && x != draggedItem).OrderBy(x => Vector2.Distance(x.worldBound.position, draggedItem.worldBound.position)).First();

            //Check to see if it's hanging over the edge - if so, do not place.
            if (!m_InventoryGrid.layout.Contains(new Vector2(draggedItem.localBound.xMax, draggedItem.localBound.yMax)))
            {
                m_GhostRect.style.visibility = Visibility.Hidden;
                return (canPlace: false, position: Vector2.zero);
            }

            m_GhostRect.style.width = draggedItem.style.width;
            m_GhostRect.style.height = draggedItem.style.height;

            m_GhostRect.style.top = targetSlot.layout.position.y;
            m_GhostRect.style.left = targetSlot.layout.position.x;

            m_GhostRect.style.visibility = Visibility.Visible;

            var overlappingItems = StoredItems.Where(x => x.RootVisual != null && x.RootVisual.layout.Overlaps(m_GhostRect.layout)).ToArray();

            if (overlappingItems.Length > 1)
            {
                m_GhostRect.style.visibility = Visibility.Hidden;
                return (canPlace: false, position: Vector2.zero);
            }
            
            return (canPlace: true, position: targetSlot.worldBound.position);

        }

        async Task<bool> CalculatePosition(VisualElement newItem)
        {

            for (int i = 0; i < InventoryDimensions.Height; i++)
            {
                for (int j = 0; j < InventoryDimensions.Width; j++)
                {
                    Vector2 newPos = new Vector2(m_SingleSlotDimension.Width * j, m_SingleSlotDimension.Height * i);

                    newItem.style.top = newPos.y;
                    newItem.style.left = newPos.x;

                    await UniTask.WaitForEndOfFrame();

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


        public void SetItemView(ItemDefinition item)
        {
            m_ItemDetailHeader.text = item.FriendlyName;
            m_ItemDetailBody.text = item.Description;
            m_ItemDetailPrice.text = item.SellPrice.ToString();
        }

    }

    [Serializable]
    public class StoredItem
    {
        public ItemDefinition Details;
        public string InstanceGuid { get; private set; } = Guid.NewGuid().ToString();
        public Vector2 Position;
        public ItemVisual RootVisual;

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
