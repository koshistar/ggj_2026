using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UI Input")]
public class UIInput : ScriptableObject, InputSystem.IUIActions
{
    public event UnityAction onClose = delegate { };
    private InputSystem inputSystem;

    private void OnEnable()
    {
        inputSystem = new InputSystem();
        inputSystem.UI.SetCallbacks(this);
    }

    public void EnableUIInput()
    {
        inputSystem.GamePlay.Disable();
        inputSystem.UI.Enable();
    }

    public void DisableUIInput()
    {
        inputSystem.UI.Disable();
    }

    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onClose.Invoke();
        }
    }
}
