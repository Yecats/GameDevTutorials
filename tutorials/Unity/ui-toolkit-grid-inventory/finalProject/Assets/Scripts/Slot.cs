using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class Slot : VisualElement
    {
        public string StoredItemInstanceId { get; private set; }

        public Slot(string elementName)
        {
            name = elementName;
            AddToClassList("visual-icon");
        }

        public void SetItemInstanceId(string id)
        {
            StoredItemInstanceId = id;
        }
    }
}
