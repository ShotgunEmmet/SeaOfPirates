using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : InventoryItem
{

    public GameObject bullet;

    public float projectileSpeed = 2f;

    public float firingRate = 0.5f;

    public override void Use(GameObject emitter, Vector3 direction)
    {
        direction.Normalize();

        var bullet = Instantiate(this.bullet);
        bullet.transform.position += emitter.transform.position + direction * 0.5f;
        bullet.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);
        //bullet.GetComponent<Bullet>().emitter = emitter;

        bullet.GetComponent<AudioSource>().Play();
    }
    
}
