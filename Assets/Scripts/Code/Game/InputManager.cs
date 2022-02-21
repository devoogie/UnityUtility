using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour,InputMain.IPlayerActions,IManager
{
    private InputMain inputSystem;
    public event System.Action onFireBegin;
    public event System.Action onFireEnd;
    public event System.Action<Vector2> onMove;
    
    public void Initialize()
    {
        if(inputSystem == null)
            inputSystem = new InputMain();
        inputSystem.Player.SetCallbacks(this);
        inputSystem.Player.Enable();
        onFireBegin = null;   
        onFireEnd = null;   
        onMove = null;    
    }
    public void OnFire(InputAction.CallbackContext context)
    {    
        if(context.started)
            onFireBegin?.Invoke();
        if(context.canceled)
            onFireEnd?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var moveDirection = context.ReadValue<Vector2>();   
        onMove?.Invoke(moveDirection);
    }
    
}