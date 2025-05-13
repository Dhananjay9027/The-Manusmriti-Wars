using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//idle
public class AimState : AimBaseState
{
     public override void EnterState(PlayerAnimation animation)
     {
        animation.animator.SetBool("Aiming", false);
     }
    public override void UpdateState(PlayerAnimation animation)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animation.SwitchState(animation.Aim);
            animation.SetAiming(true);
        }
    }
}
