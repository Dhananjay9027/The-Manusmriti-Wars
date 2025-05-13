using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState :  AimBaseState
{
    public override void EnterState(PlayerAnimation animation)
    {
        animation.animator.SetBool("Aiming", true);
    }
    public override void UpdateState(PlayerAnimation animation)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animation.SwitchState(animation.Idle);
            animation.SetAiming(false);
        }
    }
}
