using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PartyManager : MonoBehaviour
    {
        public static List<PlayerDetail> Characters;

        [Header("General Properties")]
        [SerializeField] private RectTransform _selectedPanel;
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private Camera _camera;

        //mouse position variables
        private Vector2 _mouseStartPosition = Vector2.zero;
        private bool _leftMouseButtonDown = false;
        private Vector2 _bounds;

        //button variables
        private bool _shiftButtonDown = false;
        private bool _initialPartySetup;

        void Awake()
        {
            Characters = new List<PlayerDetail>();
        }

        void Start()
        {
            if (_canvas == null)
            {
                _canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            }

            if (_selectedPanel == null)
            {
                //This call is safe since we only have a single child - if you grow your UI beyond one child, this will need to be changed.
                _selectedPanel = _canvas.GetComponentInChildren<RectTransform>();
            }

            if (_camera == null)
            {
                _camera = Camera.main;
            }
        }

        /// <summary>
        /// Monitors for whether the escape button is pushed
        /// </summary>
        public void EscapeDown_OnClick(InputAction.CallbackContext context)
        {
            //Started phase ensures that we only trigger at the start of the button push
            if (context.phase != InputActionPhase.Started)
            {
                return;
            }

            //clear out the members
            ClearAllPartyMembers();
        }

        /// <summary>
        /// Monitors for whether the shift button is pushed
        /// </summary>
        public void ShiftDown_OnClick(InputAction.CallbackContext context)
        {
            // Logs the current state of the shift button
            _shiftButtonDown = context.phase == InputActionPhase.Started;
        }

        /// <summary>
        /// Moves the selection panel to the start location of the mouse 
        /// </summary>
        /// <param name="context"></param>
        public void LeftMouseDown_OnClick(InputAction.CallbackContext context)
        {
            //record if we are pressed
            _leftMouseButtonDown = context.phase == InputActionPhase.Performed;

            //zero out the width/height of the panel if the current phase is canceled
            if (context.phase == InputActionPhase.Canceled)
            {
                _selectedPanel.sizeDelta = Vector2.zero;
                return;
            }
            // store the mouse start position
            _mouseStartPosition = Mouse.current.position.ReadValue();

            //move the select panels start position to the initial mouse click position
            Vector2 mousePos;

            //Takes a screen point and transforms it to local point within a Rect Transform (in this case, the canvas)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, _mouseStartPosition, _camera,
                out mousePos);

            _selectedPanel.localPosition = mousePos;

            //track whether this is an initial selection of the party
            _initialPartySetup = Characters.Count(x => x.IsSelected) == 0;

        }

        /// <summary>
        /// Draws the select UI visual on the screen
        /// </summary>
        private void DrawSelectWindow()
        {
            //get a vector2 based on the difference of where the mouse started and where it's at now
            _bounds = _mouseStartPosition - Mouse.current.position.ReadValue();

            //set the width of the panel based on the delta between start and end position (abs ensures it's always positive)
            _selectedPanel.sizeDelta = new Vector2(Mathf.Abs(_bounds.x), Mathf.Abs(_bounds.y));

            // Set the scale of the rect based on direction the mouse has been moved. This will flip it if we are going backwards
            float xDirection = _bounds.x < 0 ? 1 : -1;
            float yDirection = _bounds.y < 0 ? -1 : 1;

            //Set the local scale of the visual panel
            _selectedPanel.localScale = new Vector3(xDirection, yDirection, 1);
        }

        /// <summary>
        /// Toggles party member selection on/off based on the selection rectangle that was drawn by the player
        /// </summary>
        private void SelectPartyMembers()
        {
            //check to see if some party members are selected and if so, verify that the shift button is down
            if (!_initialPartySetup && !_shiftButtonDown)
            {
                return;
            }

            //Loop through all registered characters
            foreach (PlayerDetail character in Characters)
            {
                //Get a point on the UI that represents the equivalent for the collider's center point
                Vector2 screenPosition = WorldToUiSpace(character.MyCollider.bounds.center);

                //Check to see if that point is within the bounds of the selection panel

                //This is an addition to an existing party
                if (!_initialPartySetup && _shiftButtonDown)
                {
                    //Make sure we're only operating on characters who are selected. Without this our existing party members would be deselected
                    if (!character.IsSelected)
                    {
                        character.IsSelected = RectTransformUtility.RectangleContainsScreenPoint(_selectedPanel, screenPosition);
                    }
                }
                //new party creation
                else
                {
                    character.IsSelected = RectTransformUtility.RectangleContainsScreenPoint(_selectedPanel, screenPosition);
                }
            }
        }

        /// <summary>
        /// Clears the entire selection of objects
        /// </summary>
        private void ClearAllPartyMembers()
        {
            //loop through all characters and set the status to false
            foreach (PlayerDetail character in Characters)
            {
                character.IsSelected = false;
            }
        }

        void FixedUpdate()
        {
            //we're only taking action if the left mouse button is currently down
            if (!_leftMouseButtonDown)
            {
                return;
            }

            DrawSelectWindow();
            SelectPartyMembers();
        }

        /// <summary>
        /// Reads a point in world space and converts it to a world space equivalent on the UI
        /// </summary>
        /// <param name="worldPoint">Returns a UI position on the provided transform from a world position vector</param>
        private Vector2 WorldToUiSpace(Vector3 worldPoint)
        {
            //Convert the world point of the collider center to Screen Point
            Vector2 screenPoint = _camera.WorldToScreenPoint(worldPoint);

            //Convert the screen point to UI rectangle local point
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPoint, _camera, out uiPosition);

            //Convert the screen point from local to world coordinate
            return _canvas.TransformPoint(uiPosition);
        }
    }
}
