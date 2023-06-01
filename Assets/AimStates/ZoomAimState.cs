using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAimState  : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        // Set animator items
        // aim.anim.SetSmh("Smh", smh);
        aim.currentFov = aim.zoomFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(Constants.Zoom))
        {
            aim.SwitchState(aim.Normal);
        }
    }

}
