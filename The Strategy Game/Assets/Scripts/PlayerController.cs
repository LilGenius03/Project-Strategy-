using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    menu,
    defending,
    attacking,
}

public class PlayerController : MonoBehaviour
{
    public short id;

    public PlayerStates playerState;
    PlayerState_Base currentState;

    private void Start()
    {
        //currentState = state_Joined;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        currentState.OnMove(this, ctx);
    }
}
