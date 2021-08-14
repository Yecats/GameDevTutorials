using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ItemVisual : VisualElement
    {
        private ItemDefinition m_Item;
        private Vector2 m_originalPosition;
        private bool m_IsDragging;

        private (bool canPlace, Vector2 position) m_PlacementResults;

        public ItemVisual(ItemDefinition item, Dimensions singleSlotDimensions)
        {
            m_Item = item;

            VisualElement icon = new VisualElement();
            Add(icon);
            icon.style.backgroundImage = m_Item.Icon.texture;
            icon.AddToClassList("visual-icon");

            AddToClassList("visual-icon-container");
            name = $"{m_Item.FriendlyName}";
            style.height = (m_Item.SlotDimension.Height * singleSlotDimensions.Height);
            style.width = (m_Item.SlotDimension.Width * singleSlotDimensions.Width);
            style.visibility = Visibility.Hidden;

            RegisterCallback<MouseMoveEvent>(OnPointerMoveEvent);
            RegisterCallback<MouseUpEvent>(OnMouseUpEvent);

        }

        private void OnMouseUpEvent(MouseUpEvent evt)
        {
            if (!m_IsDragging)
            {
                StartDrag();
                return;
            }

            m_IsDragging = false;
            
            if (m_PlacementResults.canPlace)
            {
                style.top = m_PlacementResults.position.y - parent.worldBound.position.y;
                style.left = m_PlacementResults.position.x - parent.worldBound.position.x;
                return;
            }

            style.top = m_originalPosition.y;
            style.left = m_originalPosition.x;
        }

        public void StartDrag()
        {
            m_IsDragging = true;

            m_originalPosition = worldBound.position - parent.worldBound.position;
            PlayerInventory.Instance.SetItemView(m_Item);
            BringToFront();
        }

        private void OnPointerMoveEvent(MouseMoveEvent evt)
        {
            if (!m_IsDragging)
            {
                return;
            }

            Vector2 convertedMousePos = new Vector2(
                evt.mousePosition.x - (layout.width / 2) - parent.worldBound.position.x, 
                evt.mousePosition.y - (layout.height / 2) - parent.worldBound.position.y);

            style.top = convertedMousePos.y;
            style.left = convertedMousePos.x;

            m_PlacementResults = PlayerInventory.Instance.ShowPlacementTarget(this);
        }
    }
}
