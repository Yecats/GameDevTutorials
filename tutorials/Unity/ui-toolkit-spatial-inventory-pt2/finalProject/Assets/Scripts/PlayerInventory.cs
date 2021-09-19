using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    [Serializable]
    public class StoredItem
    {
        public ItemDefinition Details;
        public ItemVisual RootVisual;
    }

    public sealed class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance;

        private bool m_IsInventoryReady;
        public List<StoredItem> StoredItems = new List<StoredItem>();

        private VisualElement m_Root;
        private VisualElement m_InventoryGrid;
        public Dimensions InventoryDimensions;

        public static Dimensions SlotDimension { get; private set; }

        private static Label m_ItemDetailHeader;
        private static Label m_ItemDetailBody;
        private static Label m_ItemDetailPrice;

        private VisualElement m_Telegraph;

        /// <summary>
        /// Set the singleton reference and call Configure
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                Configure();
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        private void Start() => LoadInventory();

        /// <summary>
        /// Configure the UI by grabbing references and setting necessary properties
        /// </summary>
        private async void Configure()
        {
            //Set references for later
            m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;
            m_InventoryGrid = m_Root.Q<VisualElement>("Grid");
            VisualElement itemDetails = m_Root.Q<VisualElement>("ItemDetails");
            m_ItemDetailHeader = itemDetails.Q<Label>("Header");
            m_ItemDetailBody = itemDetails.Q<Label>("Body");
            m_ItemDetailPrice = itemDetails.Q<Label>("SellPrice");

            ConfigureInventoryTelegraph();

            //Wait for UI toolkit to build UI
            await UniTask.WaitForEndOfFrame();

            ConfigureSlotDimensions();

            //Inventory is now ready
            m_IsInventoryReady = true;
        }

        /// <summary>
        /// Create a visual element for the telegraph (yellow box)
        /// </summary>
        private void ConfigureInventoryTelegraph()
        {
            //Create telegraphing element
            m_Telegraph = new VisualElement
            {
                name = "Telegraph",
            };

            //set style
            m_Telegraph.AddToClassList("slot-icon-highlighted");

            //Add to UI
            AddItemToInventoryGrid(m_Telegraph);
        }

        /// <summary>
        /// Set the single slot dimension based on the total size of the grid)
        /// </summary>
        private void ConfigureSlotDimensions()
        {
            VisualElement firstSlot = m_InventoryGrid.Children().First();

            SlotDimension = new Dimensions
            {
                Width = Mathf.RoundToInt(firstSlot.worldBound.width),
                Height = Mathf.RoundToInt(firstSlot.worldBound.height)
            };
        }

        private void AddItemToInventoryGrid(VisualElement item) => m_InventoryGrid.Add(item);
        private void RemoveItemFromInventoryGrid(VisualElement item) => m_InventoryGrid.Remove(item);

        /// <summary>
        /// Load all of the inventory items and set them in the default position. 
        /// This is called from Start.
        /// </summary>
        private async void LoadInventory()
        {
            //make sure inventory is in ready state
            await UniTask.WaitUntil(() => m_IsInventoryReady);

            //load
            foreach (StoredItem loadedItem in StoredItems)
            {
                ItemVisual inventoryItemVisual = new ItemVisual(loadedItem.Details);

                AddItemToInventoryGrid(inventoryItemVisual);

                bool inventoryHasSpace = await GetPositionForItem(inventoryItemVisual);

                if (!inventoryHasSpace)
                {
                    Debug.Log("No space - Cannot pick up the item");
                    RemoveItemFromInventoryGrid(inventoryItemVisual);
                    continue;
                }

                ConfigureInventoryItem(loadedItem, inventoryItemVisual);
            }
        }

        /// <summary>
        /// Call to associate item with visual element and set active
        /// </summary>
        private static void ConfigureInventoryItem(StoredItem item, ItemVisual visual)
        {
            item.RootVisual = visual;
            visual.style.visibility = Visibility.Visible;
        }

        /// <summary>
        /// Call to set item properties on UI
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateItemDetails(ItemDefinition item)
        {
            m_ItemDetailHeader.text = item.FriendlyName;
            m_ItemDetailBody.text = item.Description;
            m_ItemDetailPrice.text = item.SellPrice.ToString();
        }

        /// <summary>
        /// Set an elements position.
        /// </summary>
        private static void SetItemPosition(VisualElement element, Vector2 vector)
        {
            element.style.left = vector.x;
            element.style.top = vector.y;
        }

        /// <summary>
        /// Finds the position for an item on first load. 
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        private async Task<bool> GetPositionForItem(VisualElement newItem)
        {
            for (int y = 0; y < InventoryDimensions.Height; y++)
            {
                for (int x = 0; x < InventoryDimensions.Width; x++)
                {
                    //try position
                    SetItemPosition(newItem, new Vector2(SlotDimension.Width * x, SlotDimension.Height * y));

                    await UniTask.WaitForEndOfFrame();

                    StoredItem overlappingItem = StoredItems.FirstOrDefault(s => s.RootVisual != null && s.RootVisual.layout.Overlaps(newItem.layout));

                    //Nothing is here! Place the item.
                    if (overlappingItem == null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check to see whether the player can place the item and if so, draw the telegraph.
        /// </summary>
        /// <returns>Whether the item can be placed in the target location and the target location</returns>
        public (bool canPlace, Vector2 position) ShowPlacementTarget(ItemVisual draggedItem)
        {
            //Check to see if it's hanging over the edge - if so, do not place.
            if (!m_InventoryGrid.layout.Contains(new Vector2(draggedItem.localBound.xMax, draggedItem.localBound.yMax)))
            {
                m_Telegraph.style.visibility = Visibility.Hidden;
                return (canPlace: false, position: Vector2.zero);
            }

            VisualElement targetSlot = m_InventoryGrid.Children().Where(x => x.layout.Overlaps(draggedItem.layout) && x != draggedItem).OrderBy(x => Vector2.Distance(x.worldBound.position, draggedItem.worldBound.position)).First();

            m_Telegraph.style.width = draggedItem.style.width;
            m_Telegraph.style.height = draggedItem.style.height;

            SetItemPosition(m_Telegraph, new Vector2(targetSlot.layout.position.x, targetSlot.layout.position.y));

            m_Telegraph.style.visibility = Visibility.Visible;

            var overlappingItems = StoredItems.Where(x => x.RootVisual != null && x.RootVisual.layout.Overlaps(m_Telegraph.layout)).ToArray();

            if (overlappingItems.Length > 1)
            {
                m_Telegraph.style.visibility = Visibility.Hidden;
                return (canPlace: false, position: Vector2.zero);
            }

            return (canPlace: true, targetSlot.worldBound.position);

        }
    }
}
