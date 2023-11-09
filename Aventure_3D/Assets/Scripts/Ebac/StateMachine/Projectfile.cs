using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectfile : MonoBehaviour
{
    public float timeToReset = 5f;
    public Vector3 dir;

    public string tagToLook = "Enemy";

    public Action OnHitTarget;


    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }


    public void StartProjectfile()
    {
        Invoke(nameof(FinishUsage), timeToReset);
    }
    
    private void FinishUsage()
    {
        gameObject.SetActive(false);
        OnHitTarget = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToLook)
        {
            Destroy(collision.gameObject);
            OnHitTarget?.Invoke();
            FinishUsage();

            //Destroy(gameObject);
        }
    }
}
