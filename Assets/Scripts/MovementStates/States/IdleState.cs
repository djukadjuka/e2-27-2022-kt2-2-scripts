using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(Constants.KeyRun))
            {
                movement.SwitchState(movement.Run);
            }
            else
            {
                movement.SwitchState(movement.Walk);
            }
        }

        if (Input.GetKey(Constants.KeyCrouch))
        {
            movement.SwitchState(movement.Crouch);
        }

        if (Input.GetKeyDown(Constants.KeyJump))
        {
            movement.previousState = this;
            movement.SwitchState(movement.Jump);
        }
    }
}
