using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.UIElements;
using UnityEngine;

namespace Assets.WUG.Scripts
{
    public class InventoryUIController : MonoBehaviour
    {
        public List<InventorySlot> InventoryItems = new List<InventorySlot>();

        private VisualElement m_Root;
        private VisualElement m_SlotContainer;
        private static VisualElement m_GhostIcon;
        
        private static bool m_IsDragging;

        private void Awake()
        {
            //Store the root from the UI Document component
            m_Root = GetComponent<UIDocument>().rootVisualElement;
            m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");

            //Search the root for the SlotContainer Visual Element
            m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");

            //Create 20 InventorySlots and add them as children to the SlotContainer
            for (int i = 0; i < 24; i++)
            {
                InventorySlot item = new InventorySlot();

                InventoryItems.Add(item);

                m_SlotContainer.Add(item);
            }

            GameController.OnInventoryChanged += GameController_OnInventoryChanged;

            m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }        
        
        public static void StartDrag(string itemGUID)
        {
            m_IsDragging = true;

           // m_GhostIcon.CapturePointer(0);

            //Set the image
            m_GhostIcon.style.backgroundImage = GameController.GetItemByGuid(itemGUID).Icon.texture;

            //Flip the y position to align with the UI coordinate system (top/left) instead of the mouse coordinate system (bottom/left)
            Vector2 newPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            //Convert the position to element-relative coordinates and set them on the element
            newPos = m_GhostIcon.WorldToLocal(newPos);

            //Set the position of the ghost icon, making sure to center the image to the mouse
            m_GhostIcon.transform.position = new Vector3(newPos.x - m_GhostIcon.layout.width / 2, newPos.y - m_GhostIcon.layout.width / 2);

            //
            m_GhostIcon.CapturePointer(0);

            //Flip the visibility on
            m_GhostIcon.style.visibility = Visibility.Visible;
            Debug.Log(Input.mousePosition);


        }
        private void OnPointerMove(PointerMoveEvent evt)
        {
            if (!m_IsDragging)
            {
                return;
            }

            //Set the new position
            // m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
            //  m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;

            Debug.Log(Input.mousePosition);
            //Flip the y position to align with the UI coordinate system (top/left) instead of the mouse coordinate system (bottom/left)
            Vector2 newPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            //Convert the position to element-relative coordinates and set them on the element
            newPos = m_GhostIcon.WorldToLocal(newPos);

            //Set the position of the ghost icon, making sure to center the image to the mouse
            //m_GhostIcon.transform.position = new Vector3(newPos.x - m_GhostIcon.layout.width / 2, newPos.y - m_GhostIcon.layout.width / 2);

            m_GhostIcon.style.top = newPos.y - m_GhostIcon.layout.height / 2;
            m_GhostIcon.style.left = newPos.x - m_GhostIcon.layout.width / 2;

            // m_GhostIcon.transform.position = new Vector3(evt.position.x - m_GhostIcon.layout.width / 2, evt.position.y - m_GhostIcon.layout.height / 2);

            //Prevent this event from going further "down" the stack
            evt.StopPropagation();

        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            if (!m_IsDragging)
            {
                return;
            }

            // m_GhostIcon.ReleasePointer(0);
            //Relase claim to the Pointer Events
            m_GhostIcon.ReleasePointer(0);

            //Clear dragging related visuals and data
            m_IsDragging = false;
            m_GhostIcon.style.visibility = Visibility.Hidden;
        }

        private void GameController_OnInventoryChanged(string[] itemGuid, InventoryChangeType change)
        {
            //Loop through each item and if it has been picked up, add it to the next empty slot
            foreach (string item in itemGuid)
            {
                if (change == InventoryChangeType.Pickup)
                {
                   var emptySlot = InventoryItems.FirstOrDefault(x => x.ItemGuid.Equals(""));
                    
                    if (emptySlot != null)
                    {
                        emptySlot.HoldItem(GameController.GetItemByGuid(item));
                    }
                }
            }
        }
    }
}
