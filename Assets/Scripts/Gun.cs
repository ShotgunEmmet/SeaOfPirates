using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : InventoryItem
{

    public GameObject bullet;

    public float firingRate = 0.5f;

    public override void Use(Vector3 position, Vector3 direction)
    {
        var newProjectile = Instantiate(bullet);
        newProjectile.transform.position += position + direction.normalized * 0.5f;
        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
    }
}
