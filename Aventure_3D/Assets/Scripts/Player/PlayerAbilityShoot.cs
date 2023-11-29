using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UIFillUpdater> uIGunUpdaters;

    public GunBase[] gunPrefabs;
    public Transform gunPosition;

    private GunBase _currentGun;
    private GunBase[] gunInstances;    
    
    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CanceltShoot();

        inputs.Gameplay.ChangeGun1.performed += cts => ChangeGun(0);
        inputs.Gameplay.ChangeGun2.performed += cts => ChangeGun(1);
    }

    private void CreateGun()
    {
        gunInstances = new GunBase[gunPrefabs.Length];

        for (int  i = 0; i < gunPrefabs.Length; ++i)
        {
            gunInstances[i] = Instantiate(gunPrefabs[i], gunPosition);
            gunInstances[i].transform.localPosition = gunInstances[i].transform.localEulerAngles = Vector3.zero;
        }

        if(gunInstances.Length > 0 )
        {
            _currentGun = gunInstances[0];
        }
    }

    private void ChangeGun(int index)
    {
        if(gunInstances == null || index >= gunInstances.Length)
        {
            return;
        }

        for (int i = 0; i < gunInstances.Length; ++i)
        {
            gunInstances[i].gameObject.SetActive(index == i);
            if(index == i)
            {
                _currentGun = gunInstances[i];
            }
        }
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }
    private void CanceltShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}
