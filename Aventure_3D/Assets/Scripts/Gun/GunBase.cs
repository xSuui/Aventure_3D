using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCoroutine;

    private bool isShooting = false; // Adiciona uma variável para rastrear se está atirando.


    #region extras
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isShooting = true; // Indica que estamos atirando.

            // Iniciamos a coroutine apenas uma vez.
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(StarShoot());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isShooting = false; // Indica que não estamos atirando mais.

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null; // Garantimos que a coroutine seja interrompida e a referência limpa.
            }
        }
    }*/
    #endregion 

    protected virtual IEnumerator ShootCoroutine()
    {
        while (isShooting) // Verificamos a variável isShooting em vez de true.
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {


        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
    }

    public void startShoot()
    {
        StopShoot();

        isShooting = true; // Indica que estamos atirando.

        // Iniciamos a coroutine apenas uma vez.
        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShoot()
    {
        isShooting = false; // Indica que não estamos atirando mais.

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null; // Garantimos que a coroutine seja interrompida e a referência limpa.
        }

    }
}



#region codigo da aula
/*
// código da aula**
public ProjectileBase prefabProjectile;

public Transform positionToShoot;
public float timeBetweenShoot = .3f;
public Transform playerSideReference;

private Coroutine _currentCoroutine;

void Update()
{
    if (Input.GetKey(KeyCode.S))
    {
        _currentCoroutine = StartCoroutine(StarShoot());

    }
    else if (Input.GetKeyUp(KeyCode.S))
    {
        if (_currentCoroutine !=null) 
            StopCoroutine(_currentCoroutine);
    }
}

IEnumerator StarShoot()
{
    while(true)
    {
        Shoot();
        yield return new WaitForSeconds(timeBetweenShoot);
    }
}



public void Shoot()
{
    var projectile = Instantiate(prefabProjectile);
    projectile.transform.position = positionToShoot.position;
    projectile.side = playerSideReference.transform.localScale.x;
}
}*/
#endregion