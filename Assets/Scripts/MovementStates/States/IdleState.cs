using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.ConfigureControllerNormal();
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(Constants.KeyRun) && movement.RunEnabled)
            {
                movement.SwitchState(movement.Run);
            }
            else
            {
                movement.SwitchState(movement.Walk);
            }
        }

        if (Input.GetKey(Constants.KeyCrouch) && movement.CrouchEnabled)
        {
            movement.SwitchState(movement.Crouch);
        }

        if (Input.GetKeyDown(Constants.KeyJump) && movement.JumpEnabled)
        {
            movement.previousState = this;
            movement.SwitchState(movement.Jump);
        }
    }
}
