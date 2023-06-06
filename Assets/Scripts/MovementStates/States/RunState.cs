using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Running", true);
        movement.ConfigureControllerNormal();
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(Constants.KeyRun))
        {
            ExitState(movement, movement.Walk);
        }
        else if(movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }


        if (movement.vtInput < 0)
        {
            movement.currentMoveSpeed = movement.runBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.runSpeed;
        }

        if (Input.GetKeyDown(Constants.KeyJump) && movement.JumpEnabled)
        {
            movement.previousState = this;
            ExitState(movement, movement.Jump);
        }
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
