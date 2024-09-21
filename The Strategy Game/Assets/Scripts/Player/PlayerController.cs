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

    public DefenseGrid defenseGrid;
    public grid_space current_grid_space;
    public AttackSpawn attack_spawn;

    private void Awake()
    {
        
    }

    private void Start()
    {
        currentState = state_defend;
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

    public void OnButtonSouth(InputAction.CallbackContext ctx)
    {
        currentState.OnButtonSouth(this, ctx);
    }

    private void OnDrawGizmos()
    {
        currentState.DrawGizmos(this);
    }
}
