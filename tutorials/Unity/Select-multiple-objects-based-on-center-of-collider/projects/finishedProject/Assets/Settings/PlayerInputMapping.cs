// GENERATED AUTOMATICALLY FROM 'Assets/Settings/PlayerInputMapping.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerInputMapping : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public PlayerInputMapping()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputMapping"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""00896658-e18c-4f09-893d-a435bf034f34"",
            ""actions"": [
                {
                    ""name"": ""Camera_Move"",
                    ""type"": ""Value"",
                    ""id"": ""c54d9dc8-6f66-4bf5-822b-eee3c9c4fe25"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera_Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""d4d64012-9b65-489f-9034-77fe913604e8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera_Rotate_Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""aa3f8f8d-8487-4b13-afd4-61509cb62d0b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera_Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""a1bad1f2-8a1b-4f81-ba72-a64e92c5f913"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Select_Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""502f8cd1-81a6-464a-ae55-f844c1efc0fb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8981797c-8614-4d51-a216-0676bd98bf86"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c81c4d49-cc26-41fc-96e6-8e39e861189c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""58989859-f5bb-48e2-abbf-dbb7801d8f5e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""85cf7a18-217d-4ea2-a3a8-b449eafeffdb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5559c9dd-a24f-43a4-b97e-34827e8c5381"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""12fcca1b-23f8-4389-86c3-fd34800f9b66"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c792ec03-4716-4f36-b433-c29ebce9ce4c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4aa3b74d-e60d-4ac5-9857-4c4e77749c2c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""24acae03-1af5-4fce-bc77-b472ae1a616f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ad87eb5f-6d76-4648-b661-56faf2d185ab"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Camera_Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""48332581-4243-46e5-99dc-49ec25e34809"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6153f071-b0a5-46d9-878b-30c6396ba287"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera_Rotate_Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e33d3faf-662e-4039-9a64-6c6fe59ebc3c"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Camera_Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c67dd34-6683-4b0b-aa1e-2d7defdf6861"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player_Select_Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Camera_Move = m_Player.FindAction("Camera_Move", throwIfNotFound: true);
        m_Player_Camera_Rotate = m_Player.FindAction("Camera_Rotate", throwIfNotFound: true);
        m_Player_Camera_Rotate_Toggle = m_Player.FindAction("Camera_Rotate_Toggle", throwIfNotFound: true);
        m_Player_Camera_Zoom = m_Player.FindAction("Camera_Zoom", throwIfNotFound: true);
        m_Player_Player_Select_Toggle = m_Player.FindAction("Player_Select_Toggle", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Camera_Move;
    private readonly InputAction m_Player_Camera_Rotate;
    private readonly InputAction m_Player_Camera_Rotate_Toggle;
    private readonly InputAction m_Player_Camera_Zoom;
    private readonly InputAction m_Player_Player_Select_Toggle;
    public struct PlayerActions
    {
        private PlayerInputMapping m_Wrapper;
        public PlayerActions(PlayerInputMapping wrapper) { m_Wrapper = wrapper; }
        public InputAction @Camera_Move => m_Wrapper.m_Player_Camera_Move;
        public InputAction @Camera_Rotate => m_Wrapper.m_Player_Camera_Rotate;
        public InputAction @Camera_Rotate_Toggle => m_Wrapper.m_Player_Camera_Rotate_Toggle;
        public InputAction @Camera_Zoom => m_Wrapper.m_Player_Camera_Zoom;
        public InputAction @Player_Select_Toggle => m_Wrapper.m_Player_Player_Select_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Camera_Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Move;
                Camera_Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Move;
                Camera_Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Move;
                Camera_Rotate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Rotate;
                Camera_Rotate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Rotate;
                Camera_Rotate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Rotate;
                Camera_Rotate_Toggle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Rotate_Toggle;
                Camera_Rotate_Toggle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Rotate_Toggle;
                Camera_Rotate_Toggle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Rotate_Toggle;
                Camera_Zoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Zoom;
                Camera_Zoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Zoom;
                Camera_Zoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera_Zoom;
                Player_Select_Toggle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayer_Select_Toggle;
                Player_Select_Toggle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayer_Select_Toggle;
                Player_Select_Toggle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayer_Select_Toggle;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Camera_Move.started += instance.OnCamera_Move;
                Camera_Move.performed += instance.OnCamera_Move;
                Camera_Move.canceled += instance.OnCamera_Move;
                Camera_Rotate.started += instance.OnCamera_Rotate;
                Camera_Rotate.performed += instance.OnCamera_Rotate;
                Camera_Rotate.canceled += instance.OnCamera_Rotate;
                Camera_Rotate_Toggle.started += instance.OnCamera_Rotate_Toggle;
                Camera_Rotate_Toggle.performed += instance.OnCamera_Rotate_Toggle;
                Camera_Rotate_Toggle.canceled += instance.OnCamera_Rotate_Toggle;
                Camera_Zoom.started += instance.OnCamera_Zoom;
                Camera_Zoom.performed += instance.OnCamera_Zoom;
                Camera_Zoom.canceled += instance.OnCamera_Zoom;
                Player_Select_Toggle.started += instance.OnPlayer_Select_Toggle;
                Player_Select_Toggle.performed += instance.OnPlayer_Select_Toggle;
                Player_Select_Toggle.canceled += instance.OnPlayer_Select_Toggle;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnCamera_Move(InputAction.CallbackContext context);
        void OnCamera_Rotate(InputAction.CallbackContext context);
        void OnCamera_Rotate_Toggle(InputAction.CallbackContext context);
        void OnCamera_Zoom(InputAction.CallbackContext context);
        void OnPlayer_Select_Toggle(InputAction.CallbackContext context);
    }
}
