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
        public ItemVisual(ItemDefinition item, Dimensions singleSlotDimensions)
        {
            style.backgroundImage = item.Icon.texture;
            AddToClassList("visual-icon");
            name = $"{item.FriendlyName}";
            style.height = (item.SlotDimension.Height * singleSlotDimensions.Height) - 10;
            style.width = (item.SlotDimension.Width * singleSlotDimensions.Width) - 10;
            style.visibility = Visibility.Hidden;
        }

    }
}
