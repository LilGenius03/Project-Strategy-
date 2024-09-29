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
        EvaluateGridSpace(controller);

        controller.my_ui.SwitchUI(false);
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
            Vector3 prev_pos = currentPosition;
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

            //Debug.Log(currentPosition);

            grid_space spais = controller.defenseGrid.FindSpace(currentPosition);

            if (spais != null)
            {
                controller.current_grid_space = spais;
                currentPosition = controller.current_grid_space.transform.position;

                EvaluateGridSpace(controller); //TEMP
            }
            else
                currentPosition = prev_pos;
            

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
                if(controller.current_grid_space.current_tower != null || (controller.defend_units[0] == 0 && controller.defend_units[1] == 0))
                    controller.defendHelperMR.material.SetColor("_EmissionColor", Color.red);
                else
                    controller.defendHelperMR.material.SetColor("_EmissionColor", Color.yellow); 
                break;
            case 2:
                if (controller.current_grid_space.current_tower != null || controller.defend_units[2] == 0)
                    controller.defendHelperMR.material.SetColor("_EmissionColor", Color.red);
                else
                    controller.defendHelperMR.material.SetColor("_EmissionColor", Color.yellow);
                break;
        }
    }

    public override void OnButtonSouth(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
/*            if (controller.current_grid_space != null)
            {
                if (controller.HasMen())
                {
                    controller.LooseMan();
                    controller.defenseGrid.PlaceTower(controller.current_grid_space, controller.units.defensive_units[0], 1);
                    EvaluateGridSpace(controller);//TEMP
                }
                 //TEMP
            }*/

            if (!controller.isPrepping)
            {

                if (controller.defend_units[0] > 0)
                {
                    if (controller.current_grid_space != null)
                    {
                            controller.defend_units[0]--;
                            controller.defenseGrid.PlaceTower(controller.current_grid_space, controller.units.defensive_units[0], 1);
                    }
                }
            }
            else
            {
                if (controller.gold >= controller.defend_units_cost[0])
                {
                    controller.defend_units[0]++;
                    controller.LooseGold(controller.defend_units_cost[0]);
                }
                //buy some dude
            }
            EvaluateGridSpace(controller);
        }
            
    }

    public override void OnButtonWest(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!controller.isPrepping)
            {

                if (controller.defend_units[1] > 0)
                {
                    if (controller.current_grid_space != null)
                    {
                        controller.defend_units[1]--;
                        controller.defenseGrid.PlaceTower(controller.current_grid_space, controller.units.defensive_units[1], 1);
                    }
                }
            }
            else
            {
                if (controller.gold >= controller.defend_units_cost[1])
                {
                    controller.defend_units[1]++;
                    controller.LooseGold(controller.defend_units_cost[1]);
                }
                //buy some dude
            }
            EvaluateGridSpace(controller);
        }

    }

    public override void OnButtonEast(PlayerController controller, InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!controller.isPrepping)
            {

                if (controller.defend_units[2] > 0)
                {
                    if (controller.current_grid_space != null)
                    {
                        controller.defend_units[2]--;
                        controller.defenseGrid.PlaceTower(controller.current_grid_space, controller.units.defensive_units[2], 2);
                    }
                }
            }
            else
            {
                if (controller.gold >= controller.defend_units_cost[2])
                {
                    controller.defend_units[2]++;
                    controller.LooseGold(controller.defend_units_cost[2]);
                }
                //buy some dude
            }
            EvaluateGridSpace(controller);
        }

    }
}
