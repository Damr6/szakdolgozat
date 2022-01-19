// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""875c43fd-eab3-4fd4-94d5-accc1051a8db"",
            ""actions"": [
                {
                    ""name"": ""MouseLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""14fd9042-2b2a-438c-bf29-6182fdf4d19e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,SlowTap""
                },
                {
                    ""name"": ""MouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""7fca197b-2ee2-4c62-a6d2-c17c6bb56d37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""4873f373-9b3c-4859-b2ba-da27341e1f48"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""67e02662-435b-4653-ba3f-a9f6ecb33825"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0a595ab7-647e-4589-92da-9132e4473800"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""428e9755-3f37-4349-9a41-3e6db331098a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e98f632-46b9-43f7-a919-6c0803014146"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8086343b-3826-412e-b531-63ffc8748f10"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MouseLeftClick = m_Gameplay.FindAction("MouseLeftClick", throwIfNotFound: true);
        m_Gameplay_MouseRightClick = m_Gameplay.FindAction("MouseRightClick", throwIfNotFound: true);
        m_Gameplay_MousePosition = m_Gameplay.FindAction("MousePosition", throwIfNotFound: true);
        m_Gameplay_Tab = m_Gameplay.FindAction("Tab", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MouseLeftClick;
    private readonly InputAction m_Gameplay_MouseRightClick;
    private readonly InputAction m_Gameplay_MousePosition;
    private readonly InputAction m_Gameplay_Tab;
    public struct GameplayActions
    {
        private @PlayerInput m_Wrapper;
        public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLeftClick => m_Wrapper.m_Gameplay_MouseLeftClick;
        public InputAction @MouseRightClick => m_Wrapper.m_Gameplay_MouseRightClick;
        public InputAction @MousePosition => m_Wrapper.m_Gameplay_MousePosition;
        public InputAction @Tab => m_Wrapper.m_Gameplay_Tab;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MouseLeftClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLeftClick;
                @MouseLeftClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLeftClick;
                @MouseLeftClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLeftClick;
                @MouseRightClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseRightClick;
                @MousePosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @Tab.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTab;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLeftClick.started += instance.OnMouseLeftClick;
                @MouseLeftClick.performed += instance.OnMouseLeftClick;
                @MouseLeftClick.canceled += instance.OnMouseLeftClick;
                @MouseRightClick.started += instance.OnMouseRightClick;
                @MouseRightClick.performed += instance.OnMouseRightClick;
                @MouseRightClick.canceled += instance.OnMouseRightClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMouseLeftClick(InputAction.CallbackContext context);
        void OnMouseRightClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
    }
}
