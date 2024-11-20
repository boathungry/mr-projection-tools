//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Packages/com.envalys.mr-projection-tools/Runtime/Scripts/Sun Position/Sun Input Actions.inputactions
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

public partial class @SunInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @SunInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Sun Input Actions"",
    ""maps"": [
        {
            ""name"": ""Sun"",
            ""id"": ""ddf0d663-2fff-465d-b0e2-329ec28a1653"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""865fd79c-d1ab-431d-9492-431203c98951"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Set Date"",
                    ""type"": ""Button"",
                    ""id"": ""eb50b3f9-f3d0-415c-9a6d-47646df56a11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""eea3b6e4-bb89-4d1f-bcb5-d0309d1f5599"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8687458c-33f7-4ce7-af5a-63f165e607ea"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cc1e523d-6423-44aa-a480-0eedf4b1704e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4ada32ee-a7cd-4aea-9b13-b78d55b0dd74"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Set Date"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Sun
        m_Sun = asset.FindActionMap("Sun", throwIfNotFound: true);
        m_Sun_Move = m_Sun.FindAction("Move", throwIfNotFound: true);
        m_Sun_SetDate = m_Sun.FindAction("Set Date", throwIfNotFound: true);
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

    // Sun
    private readonly InputActionMap m_Sun;
    private List<ISunActions> m_SunActionsCallbackInterfaces = new List<ISunActions>();
    private readonly InputAction m_Sun_Move;
    private readonly InputAction m_Sun_SetDate;
    public struct SunActions
    {
        private @SunInput m_Wrapper;
        public SunActions(@SunInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Sun_Move;
        public InputAction @SetDate => m_Wrapper.m_Sun_SetDate;
        public InputActionMap Get() { return m_Wrapper.m_Sun; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SunActions set) { return set.Get(); }
        public void AddCallbacks(ISunActions instance)
        {
            if (instance == null || m_Wrapper.m_SunActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SunActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @SetDate.started += instance.OnSetDate;
            @SetDate.performed += instance.OnSetDate;
            @SetDate.canceled += instance.OnSetDate;
        }

        private void UnregisterCallbacks(ISunActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @SetDate.started -= instance.OnSetDate;
            @SetDate.performed -= instance.OnSetDate;
            @SetDate.canceled -= instance.OnSetDate;
        }

        public void RemoveCallbacks(ISunActions instance)
        {
            if (m_Wrapper.m_SunActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISunActions instance)
        {
            foreach (var item in m_Wrapper.m_SunActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SunActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SunActions @Sun => new SunActions(this);
    public interface ISunActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSetDate(InputAction.CallbackContext context);
    }
}
