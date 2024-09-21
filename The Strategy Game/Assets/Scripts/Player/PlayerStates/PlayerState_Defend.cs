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
            if(input_vector.x > 0.5f)
            {
                currentPosition.x += Mathf.Ceil(input_vector.x);
                if (currentPosition.x >= controller.defenseGrid.grid_size)
                    currentPosition.x = controller.defenseGrid.grid_size - 1;
            }
            else if (input_vector.x < -0.5f)
            {
                currentPosition.x += Mathf.Floor(input_vector.x);
                if (currentPosition.x <= 0)
                    currentPosition.x = 0;
            }
                
            if (input_vector.y > 0.5f)
            {
                currentPosition.y += Mathf.Ceil(input_vector.y);
                if (currentPosition.y >= controller.defenseGrid.grid_size)
                    currentPosition.y = controller.defenseGrid.grid_size - 1;
            }               
            else if (input_vector.y < -0.5f)
            {
                currentPosition.y += Mathf.Floor(input_vector.y);
                if (currentPosition.y <= 0)
                    currentPosition.y = 0;
            }
                



            controller.current_grid_space = controller.defenseGrid.FindSpace(currentPosition);

            currentPosition = controller.current_grid_space.position;

            //Debug.Log(currentPosition);
            //Debug.Log(input_vector);
        }
    }

    public override void OnButtonSouth(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            controller.defenseGrid.PlaceTower(controller.current_grid_space);
    }

    public override void DrawGizmos(PlayerController controller)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(currentPosition.x, 2.5f, currentPosition.y), new Vector3(0.1f, 5f, 0.1f));
    }

}
