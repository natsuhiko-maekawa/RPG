//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.1
//     from Assets/InputActions/BattleSceneInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace BattleScene.Views.InputActions
{
    public partial class @BattleSceneInputAction: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @BattleSceneInputAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""BattleSceneInputAction"",
    ""maps"": [
        {
            ""name"": ""BattleScene"",
            ""id"": ""919832c6-3ab4-4e5c-9c9c-c67ae946d377"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""61428896-d910-4e15-8b32-9e627ba31bb4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""5718c355-1550-44b1-919e-a5c8a1b4ca8c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveCursor"",
                    ""type"": ""Value"",
                    ""id"": ""5dc377b9-b381-4d6e-a998-d3031a9970cd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24ba469e-1841-4571-9016-c6731dd67fd0"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55cf7144-5147-4cca-b5c6-86895895b654"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""821cfe28-1f83-4b38-9bdb-2b3d2b55002b"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6180eff-3982-4204-b665-a2866dcda626"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de463784-f79e-44ea-94d2-5b5884f68db3"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""efbbb833-ea63-4d24-aae6-63727398a9e4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b30454d9-73f6-40be-8d00-9c091d2278d6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9a9b34ce-bbaf-41cc-8cce-482f2bd2a2c1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""998d3caf-37cb-4552-a28f-99744cd27f58"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fd7e2817-8dbd-41a3-a735-eb69c998f6ca"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1ec4769d-790b-4ed6-bd2e-a57658379a4a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cc7b1fa7-42b4-483c-9396-ea7f90177a24"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d8e1acfd-cace-43bb-b9cc-9a117d53c2ba"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""969d99d2-f849-49e8-8f78-91f5d0b5008f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""73b56ee4-5d60-4423-9adf-f506c8b1753b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // BattleScene
            m_BattleScene = asset.FindActionMap("BattleScene", throwIfNotFound: true);
            m_BattleScene_Select = m_BattleScene.FindAction("Select", throwIfNotFound: true);
            m_BattleScene_Cancel = m_BattleScene.FindAction("Cancel", throwIfNotFound: true);
            m_BattleScene_MoveCursor = m_BattleScene.FindAction("MoveCursor", throwIfNotFound: true);
        }

        ~@BattleSceneInputAction()
        {
            UnityEngine.Debug.Assert(!m_BattleScene.enabled, "This will cause a leak and performance issues, BattleSceneInputAction.BattleScene.Disable() has not been called.");
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // BattleScene
        private readonly InputActionMap m_BattleScene;
        private List<IBattleSceneActions> m_BattleSceneActionsCallbackInterfaces = new List<IBattleSceneActions>();
        private readonly InputAction m_BattleScene_Select;
        private readonly InputAction m_BattleScene_Cancel;
        private readonly InputAction m_BattleScene_MoveCursor;
        public struct BattleSceneActions
        {
            private @BattleSceneInputAction m_Wrapper;
            public BattleSceneActions(@BattleSceneInputAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @Select => m_Wrapper.m_BattleScene_Select;
            public InputAction @Cancel => m_Wrapper.m_BattleScene_Cancel;
            public InputAction @MoveCursor => m_Wrapper.m_BattleScene_MoveCursor;
            public InputActionMap Get() { return m_Wrapper.m_BattleScene; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BattleSceneActions set) { return set.Get(); }
            public void AddCallbacks(IBattleSceneActions instance)
            {
                if (instance == null || m_Wrapper.m_BattleSceneActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_BattleSceneActionsCallbackInterfaces.Add(instance);
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @MoveCursor.started += instance.OnMoveCursor;
                @MoveCursor.performed += instance.OnMoveCursor;
                @MoveCursor.canceled += instance.OnMoveCursor;
            }

            private void UnregisterCallbacks(IBattleSceneActions instance)
            {
                @Select.started -= instance.OnSelect;
                @Select.performed -= instance.OnSelect;
                @Select.canceled -= instance.OnSelect;
                @Cancel.started -= instance.OnCancel;
                @Cancel.performed -= instance.OnCancel;
                @Cancel.canceled -= instance.OnCancel;
                @MoveCursor.started -= instance.OnMoveCursor;
                @MoveCursor.performed -= instance.OnMoveCursor;
                @MoveCursor.canceled -= instance.OnMoveCursor;
            }

            public void RemoveCallbacks(IBattleSceneActions instance)
            {
                if (m_Wrapper.m_BattleSceneActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IBattleSceneActions instance)
            {
                foreach (var item in m_Wrapper.m_BattleSceneActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_BattleSceneActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public BattleSceneActions @BattleScene => new BattleSceneActions(this);
        public interface IBattleSceneActions
        {
            void OnSelect(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
            void OnMoveCursor(InputAction.CallbackContext context);
        }
    }
}
