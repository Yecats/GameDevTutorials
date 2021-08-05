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

        internal void ConfigureVisuals(ItemDefinition item, Vector2 pos)
        {
            //Set the item size
            parent.style.height = item.Dimensions.Height * 100;
            parent.style.width = item.Dimensions.Width * 100;

            //Set the parent position
            parent.style.top = pos.y;
            parent.style.left = pos.x;
        }

    }
}
