using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Attack : PlayerState_Base
{
    int currentSpawnPosition;

    int currentSelectedUnit;

    bool isMoving;

    public override void EnterState(PlayerController controller)
    {
        controller.defendHelper.SetActive(false);
    }

    public override void ExitState(PlayerController controller)
    {

    }

    public override void FrameUpdate(PlayerController controller)
    {

    }

    public override void PhysicsUpdate(PlayerController controller)
    {

    }


    public override void OnMove(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isMoving && (ctx.ReadValue<Vector2>().x > 0.5 || ctx.ReadValue<Vector2>().x < -0.5))
        {
            isMoving = true;

            if (ctx.ReadValue<Vector2>().x > 0.5)
                currentSpawnPosition++;
            
            if (ctx.ReadValue<Vector2>().x < -0.5)
                currentSpawnPosition--;

            if (currentSpawnPosition >= controller.attack_spawn.spawnPositions.Length)
                currentSpawnPosition = 0;

            if (currentSpawnPosition < 0)
                currentSpawnPosition = controller.attack_spawn.spawnPositions.Length - 1;
            Debug.Log(currentSpawnPosition);
        }

        if (ctx.canceled)
            isMoving = false;
    }

    public override void OnButtonSouth(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject littleDude = controller.HelpInstantiate(controller.units.attacker_basic, controller.attack_spawn.spawnPositions[currentSpawnPosition].position, Quaternion.identity, controller.attack_spawn.spawnPositions[currentSpawnPosition]);
            littleDude.GetComponent<AttackingDude>().AddCastleAsTarget(controller.enemyCastle);
        }
    }

    public override void DrawGizmos(PlayerController controller)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(controller.attack_spawn.spawnPositions[currentSpawnPosition].position.x, 2.5f, controller.attack_spawn.spawnPositions[currentSpawnPosition].position.z), new Vector3(0.1f, 5f, 0.1f));
    }
}
