
using Assets;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Crouching", true);
        movement.ConfigureControllerCrouch();
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(Constants.KeyRun) && movement.RunEnabled)
        {
            ExitState(movement, movement.Run);
        }
        else if (Input.GetKeyUp(Constants.KeyCrouch))
        {
            if(movement.dir.magnitude < 0.1f)
            {
                ExitState(movement, movement.Idle);
            }
            else
            {
                ExitState(movement, movement.Walk);
            }
        }


        if (movement.vtInput < 0)
        {
            movement.currentMoveSpeed = movement.crouchBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.crouchSpeed;
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}
