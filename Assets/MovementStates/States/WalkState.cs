using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(Constants.KeyRun))
        {
            ExitState(movement, movement.Run);
        }
        else if (Input.GetKey(Constants.KeyCrouch))
        {
            ExitState(movement, movement.Crouch);
        }
        else if(movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle); 
        }

        if (movement.vtInput < 0)
        {
            movement.currentMoveSpeed = movement.walkBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.walkSpeed;
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
