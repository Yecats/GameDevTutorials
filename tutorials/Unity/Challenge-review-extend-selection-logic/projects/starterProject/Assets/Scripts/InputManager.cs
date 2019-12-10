using Assets.Scripts;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CameraController CameraController;
    public PartyManager PartyManager;
    private PlayerInputMapping _input;

    void Awake()
    {
        _input = new PlayerInputMapping();

        if (CameraController == null)
        {
            CameraController = FindObjectOfType<CameraController>();
        }

        if (PartyManager == null)
        {
            PartyManager = FindObjectOfType<PartyManager>();
        }

        //Hook up callback methods
        _input.Player.Camera_Move.performed += CameraController.OnMove;
        _input.Player.Camera_Move.canceled += CameraController.OnMove;

        _input.Player.Camera_Rotate.performed += CameraController.OnRotate;
        _input.Player.Camera_Rotate.canceled += CameraController.OnRotate;

        _input.Player.Camera_Rotate_Toggle.performed += CameraController.OnRotateToggle;
        _input.Player.Camera_Rotate_Toggle.canceled += CameraController.OnRotateToggle;

        _input.Player.Camera_Zoom.performed += CameraController.OnZoom;

        _input.Player.Player_Select_Toggle.performed += PartyManager.LeftMouseDown_OnClick;
        _input.Player.Player_Select_Toggle.canceled += PartyManager.LeftMouseDown_OnClick;
    }

    void OnEnable()
    {
        //Enable all actions under the Player action map
        // You can do this on an individual action level by calling _input.Player.Camera_Move.Enable() instead.
        _input.Player.Enable();
    }

    void OnDisable()
    {
        //Disable all actions under the Player action map
        _input.Player.Disable();
    }

}
