using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableCoin : ItemCollectableBase
{
    public new Collider collider;

    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
        collider.enabled = false;
        
    }
}

#region code hyper casual
/*
//public Collider2D collider;
public new Collider collider;
public bool collect = false;
public float lerp = 5f;
public float minDistance = 1f;

private void Start()
{
    //CoinsAnimationManager.Instance.RegisterCoin(this);
}


protected override void OnCollect()
{
    base.OnCollect();
    collider.enabled = false;
    collect = true;
    //PlayerController.Instance.Bounce();
}

protected override void Collect()
{
    OnCollect();
}

private void Update()
{
    if(collect)
    {
        //transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);

        //if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
        {
            HideItens();
            Destroy(gameObject);
        }
    }
}*/
#endregion