using TMPro;
using UnityEditor.VersionControl;
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

    bool freeze_movement = true;
    public bool isPrepping;

    public UnitHelper units;

    //defend
    public DefenseGrid defenseGrid;
    public grid_space current_grid_space;
    public GameObject defendHelper;
    public MeshRenderer defendHelperMR;

    //attack
    public AttackSpawn attack_spawn;
    public Transform enemyCastle;
    public GameObject attackHelper;

    public int men;
    public int[] attack_units = new int[3]; // 0 - standard, 1 - bigguy, 2 - runner
    public int[] attack_units_cost = new int[3]; // 0 - standard, 1 - bigguy, 2 - runner
    public int[] defend_units = new int[3]; // 0 - standard, 1 - bomber, 2 - obstacle
    public int[] defend_units_cost = new int[3]; // 0 - standard, 1 - bomber, 2 - obstacle
    //public int menMax;
    int purchasedMen;
    public int gold;

    public PlayerUI my_ui;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        GameManager.OnFreezePlayers += Freeze;
        GameManager.OnUnFreezePlayers += UnFreeze;
        GameManager.OnPrepBegin += Switch2Prep;
        GameManager.OnCombatBegin += Switch2Combat;
    }

    private void OnDisable()
    {
        GameManager.OnFreezePlayers -= Freeze;
        GameManager.OnUnFreezePlayers -= UnFreeze;
        GameManager.OnPrepBegin -= Switch2Prep;
        GameManager.OnCombatBegin -= Switch2Combat;
    }

    public void Freeze()
    {
        freeze_movement = true;
    }

    public void UnFreeze()
    {
        freeze_movement = false;
    }

    public void Switch2Prep()
    {
        isPrepping = true;
    }

    public void Switch2Combat()
    {
        isPrepping = false;
    }

    private void Start()
    {
        if(id == 1)
            currentState = state_defend;
        else
            currentState = state_attack;
        currentState.EnterState(this);
    }

    public void ResetMen()
    {
        purchasedMen = 0;
    }

    public void LooseMan()
    {
        men--;
        purchasedMen++;
    }

    public void LoosePurMan()
    {
        purchasedMen--;
    }

    public bool HasMen()
    {
        return men > 0;
    }

    public bool HasPurMen()
    {
        return purchasedMen > 0;
    }

    public void LooseGold(int g)
    {
        gold-=g;
    }
    public void AddGold(int g)
    {
        gold+=g;
    }
    public bool HasGold()
    {
        return gold > 0;
    }

    public void UImanage()
    {
        my_ui.ManagePlayerUI(gold, attack_units, defend_units);
    }

    private void Update()
    {
        UImanage();
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {

        currentState.PhysicsUpdate(this);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!freeze_movement)
            currentState.OnMove(this, ctx);
    }

    public void OnButtonSouth(InputAction.CallbackContext ctx)
    {
        if(!freeze_movement)
            currentState.OnButtonSouth(this, ctx);
    }

    public void OnButtonWest(InputAction.CallbackContext ctx)
    {
        if (!freeze_movement)
            currentState.OnButtonWest(this, ctx);
    }

    public void OnButtonEast(InputAction.CallbackContext ctx)
    {
        if (!freeze_movement)
            currentState.OnButtonEast(this, ctx);
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
        currentState.EnterState(this);
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
