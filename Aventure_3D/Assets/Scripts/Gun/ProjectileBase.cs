using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float timeToDestroy = 2f;

    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    public List<string> tagsToIgnore;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f); // Ajuste este valor conforme necess�rio
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (tagsToIgnore.Contains(collision.gameObject.tag))
        {
            return;
        }

        foreach(var t in tagsToHit)
        {
            if(collision.transform.tag == t)
            { 

                var damageable = collision.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount, dir);
                }

                StartCoroutine(DelayedDestroy()); // Usar co-rotina para atrasar a destrui��o
                return;
                //break;

            }

        }
        //Destroy(gameObject);
    }

}
