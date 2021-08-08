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
        public ItemVisual(ItemDefinition item)
        {
            style.backgroundImage = item.Icon.texture;
            AddToClassList("visual-icon");
            name = $"{item.FriendlyName}";
        }

        internal void SetSize(int width, int height)
        {
            //Set the item size
            parent.style.height = height;
            parent.style.width = width;
            parent.AddToClassList("visual-icon-parent");

        }

    }
}
