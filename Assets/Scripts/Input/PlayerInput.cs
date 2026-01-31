using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputSystem.IGamePlayActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    public event UnityAction onParry = delegate { };
    public event UnityAction onUseMask = delegate { };
    public event UnityAction onPause = delegate { };
    private InputSystem inputSystem;

    private void OnEnable()
    {
        inputSystem = new InputSystem();
        inputSystem.GamePlay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs()
    {
        inputSystem.GamePlay.Disable();
        inputSystem.UI.Disable();
    }

    public void EnableGameplayInput()
    {
        inputSystem.UI.Disable();
        inputSystem.GamePlay.Enable();
        // Is Cursor?
    }

    public void EnableUIInput()
    {
        inputSystem.GamePlay.Disable();
        inputSystem.UI.Enable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            onStopMove.Invoke();
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onParry.Invoke();
        }
    }

    public void OnUseMask(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onUseMask.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onPause.Invoke();
        }
    }
}
