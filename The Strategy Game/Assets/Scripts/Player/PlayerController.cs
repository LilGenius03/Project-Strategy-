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

    //defend
    public DefenseGrid defenseGrid;
    public grid_space current_grid_space;
    public GameObject defendHelper;
    public MeshRenderer defendHelperMR;

    //attack
    public AttackSpawn attack_spawn;
    public GameObject temp_attackingdude;
    public Transform enemyCastle;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if(id == 1)
            currentState = state_defend;
        else
            currentState = state_attack;
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

    public void SwitchState(int state)
    {
        currentState.ExitState(this);
        switch (state)
        {
            case 0:
                currentState = state_menu;
                playerState = PlayerStates.menu;
                break;
            case 1:
                currentState = state_defend;
                playerState = PlayerStates.defending;
                break;
            case 2:
                currentState = state_attack;
                playerState = PlayerStates.attacking;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        //currentState.DrawGizmos(this);
    }

    public GameObject HelpInstantiate(GameObject object2Instantiate, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (parent != null)
            return Instantiate(object2Instantiate, position, rotation, parent);
        else
            return Instantiate(object2Instantiate, position, rotation);
    }
}
