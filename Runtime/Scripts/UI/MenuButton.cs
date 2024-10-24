using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{
    public GameObject menu;
    public InputActionReference openMenu;



    
    private void Awake()
    {
        openMenu.action.Enable();
        openMenu.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    
    private void OnDestroy()
    {
        openMenu.action.Disable();
        openMenu.action.performed -= ToggleMenu;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        menu.SetActive(!menu.activeSelf);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                openMenu.action.Disable();
                openMenu.action.performed -= ToggleMenu;
                break;
            case InputDeviceChange.Reconnected:
                openMenu.action.Enable();
                openMenu.action.performed += ToggleMenu;
                break;
        }
    }
}
