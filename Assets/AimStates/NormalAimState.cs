using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        // Set animator items
        // aim.anim.SetSmh("Smh", smh);
        aim.currentFov = aim.normalFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(Constants.Zoom))
        {
            aim.SwitchState(aim.Zoom);
        }
    }

}
