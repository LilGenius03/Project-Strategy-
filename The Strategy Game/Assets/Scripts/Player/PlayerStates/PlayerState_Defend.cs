using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Defend : PlayerState_Base
{
    public Vector2 currentPosition;

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
        if (ctx.performed)
        {
            Vector2 input_vector = ctx.ReadValue<Vector2>();
            Vector2 new_pos = currentPosition;
            if(input_vector.x > 0.5)
                new_pos.x += Mathf.Ceil(input_vector.x);
            else if (input_vector.x < 0.5)
                new_pos.x += Mathf.Ceil(input_vector.x);
            if (input_vector.y > 0.5)
                new_pos.y += Mathf.Ceil(input_vector.y);
            else if (input_vector.y < 0.5)
                new_pos.y += Mathf.Ceil(input_vector.y);

            //controller.defenseGrid.FindSpace(new_pos);

            Debug.Log(currentPosition);
        }
    }
}
