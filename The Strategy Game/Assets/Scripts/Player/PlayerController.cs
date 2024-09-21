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
    public PlayerState_Menu state_menu = new PlayerState_Menu();
    public PlayerState_Defend state_defend = new PlayerState_Defend();
    public PlayerState_Attack state_attack = new PlayerState_Attack();


    private void Awake()
    {
        
    }

    private void Start()
    {
        currentState = state_menu;
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
