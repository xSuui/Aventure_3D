using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> uIGunUpdaters;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentshoots;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;


        while(true)
        {
            if(_currentshoots < maxShoot)
            {
                Shoot();
                _currentshoots++;
                UpdateUI();
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else
            {
                yield break;
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentshoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }
    
    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentshoots = 0;
        _recharging = false;
            
        
        //Debug.Log("Rechargin:" + time);
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentshoots));
    }

    private void GetAllUIs()
    {
        uIGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
