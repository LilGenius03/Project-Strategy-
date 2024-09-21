using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState_Base
{
    public abstract void EnterState(PlayerController controller);
    public abstract void ExitState(PlayerController controller);

    public virtual void FrameUpdate(PlayerController controller)
    {

    }

    public virtual void PhysicsUpdate(PlayerController controller)
    {

    }

    public virtual void OnMove(PlayerController controller, InputAction.CallbackContext ctx)
    {

    }
}
