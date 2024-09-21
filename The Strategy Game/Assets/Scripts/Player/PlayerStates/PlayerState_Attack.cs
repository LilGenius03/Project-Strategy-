using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Attack : PlayerState_Base
{
    int currentSpawnPosition;

    int currentSelectedUnit;

    bool isMoving;

    public override void EnterState(PlayerController controller)
    {

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
            PlayerManager.instance.debugCube.position = controller.attack_spawn.spawnPositions[currentSpawnPosition].position;
        }

        if (ctx.canceled)
            isMoving = false;
    }

    public override void OnButtonSouth(PlayerController controller, InputAction.CallbackContext ctx)
    {
        
    }
}
