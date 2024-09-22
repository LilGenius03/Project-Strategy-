using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerState_Defend : PlayerState_Base
{
    public Vector3 currentPosition;

    public override void EnterState(PlayerController controller)
    {
        controller.defendHelper.SetActive(true);
        controller.attackHelper.SetActive(false);

        currentPosition = controller.defenseGrid.player_start_pos;

        controller.current_grid_space = controller.defenseGrid.FindSpace(currentPosition);

        currentPosition = controller.current_grid_space.transform.position;
        controller.defendHelper.transform.position = new Vector3(currentPosition.x, 2.5f, currentPosition.z);
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
                if (currentPosition.x >= controller.defenseGrid.playable_space_x_max)
                    currentPosition.x = controller.defenseGrid.playable_space_x_max - 1;
            }
            else if (input_vector.x < -0.5f)
            {
                currentPosition.x += Mathf.Floor(input_vector.x);
                if (currentPosition.x <= controller.defenseGrid.playable_space_x_min)
                    currentPosition.x = controller.defenseGrid.playable_space_x_min;
            }
                
            if (input_vector.y > 0.5f)
            {
                currentPosition.z += Mathf.Ceil(input_vector.y);
                if (currentPosition.z >= controller.defenseGrid.playable_space_y_max)
                    currentPosition.z = controller.defenseGrid.playable_space_y_max - 1;
            }               
            else if (input_vector.y < -0.5f)
            {
                currentPosition.z += Mathf.Floor(input_vector.y);
                if (currentPosition.z <= controller.defenseGrid.playable_space_y_min)
                    currentPosition.z = controller.defenseGrid.playable_space_y_min;
            }

            Debug.Log(currentPosition);

            controller.current_grid_space = controller.defenseGrid.FindSpace(currentPosition);

            if(controller.current_grid_space != null)
            {
                currentPosition = controller.current_grid_space.transform.position;

                            EvaluateGridSpace(controller); //TEMP
            }
            

            controller.defendHelper.transform.position = new Vector3(currentPosition.x, 2.5f, currentPosition.z);
        }
    }

    //TEMP
    public void EvaluateGridSpace(PlayerController controller)
    {
        switch (controller.current_grid_space.type_of_space)
        {
            case 0:
                controller.defendHelperMR.material.SetColor("_EmissionColor", Color.red);
                break;
            case 1:
                if(controller.current_grid_space.current_tower != null)
                    controller.defendHelperMR.material.SetColor("_EmissionColor", Color.red);
                else
                    controller.defendHelperMR.material.SetColor("_EmissionColor", Color.yellow); 
                break;
            case 2:
                controller.defendHelperMR.material.SetColor("_EmissionColor", Color.red);
                break;
        }
    }

    public override void OnButtonSouth(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (controller.current_grid_space != null)
            {
                controller.defenseGrid.PlaceTower(controller.current_grid_space, controller.units.tower_basic);
                EvaluateGridSpace(controller); //TEMP
            }
           
        }
            
    }
}
