using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += cts => startShoot();
        inputs.Gameplay.Shoot.canceled += cts => canceltShoot();
    }

    private void startShoot()
    {
        gunBase.startShoot();
        Debug.Log("Start Shoot");
    }
    private void canceltShoot()
    {
        Debug.Log("Cancel Shoot");
        gunBase.StopShoot();
    }
}
