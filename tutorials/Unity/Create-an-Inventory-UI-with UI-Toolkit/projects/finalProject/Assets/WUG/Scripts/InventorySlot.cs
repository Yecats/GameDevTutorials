using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Assets.WUG.Scripts
{
    public class InventorySlot : VisualElement
    {
        public Image Icon;
        public string ItemGuid = "";

        public InventorySlot()
        {
            //Create a new Image element and add it to the root
            Icon = new Image();
            Add(Icon);

            //Add USS style properties to the elements
            Icon.AddToClassList("slotIcon");
            AddToClassList("slotContainer");

            //Register event listeners
            RegisterCallback<PointerDownEvent>(OnPointerDown);
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            //Not the left mouse button or this is an empty slotIn
            if (evt.button != 0 || ItemGuid.Equals(""))
            {
                return;
            }

            //Clear the image
            Icon.image = null;

            //Start the drag
            InventoryUIController.StartDrag(evt.position, this);
            
        }

        /// <summary>
        /// Sets the Icon and GUID properties
        /// </summary>
        /// <param name="item"></param>
        public void HoldItem(ItemDetails item)
        {
            Icon.image = item.Icon.texture;
            ItemGuid = item.GUID;
        }

        /// <summary>
        /// Clears the Icon and GUID properties
        /// </summary>
        public void DropItem()
        {
            ItemGuid = "";
            Icon.image = null;
        }

        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion

    }
}
