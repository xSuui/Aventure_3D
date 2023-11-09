using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject projectfile;
    public Transform shootPoint;
    public Vector3 dir;

    public PoolManager poolManager;

    //public int deathNumber = 0;

    public bool canMove = false;

    public MeshRenderer meshRenderer;

    public void ChangeColor(Color c)
    {
        meshRenderer.material.SetColor("_Color", c);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }


    void Update()
    {
        if (!canMove) return;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(dir * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-dir * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }

    }

    private void SpawnObject()
    {
        var obj = poolManager.GetPoolEdObject();

        obj.SetActive(true);
        obj.GetComponent<Projectfile>().StartProjectfile();
        //obj.GetComponent<Projectfile>().OnHitTarget = CountDeaths;
        //obj.transform.SetParent(null);

        //var obj = Instantiate(projectfile);
        obj.transform.position = shootPoint.transform.position;
    }

    /*private void CountDeaths()
    {
        deathNumber++;
        Debug.Log("Count "+deathNumber);
    }*/
}
