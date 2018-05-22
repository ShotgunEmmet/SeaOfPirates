using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : InventoryItem
{

    public GameObject cannonBall;

    public float projectileSpeed = 2f;

    public float firingRate = 0.5f;

    public override void Use(GameObject emitter, Vector3 direction)
    {
        direction.Normalize();

        var newProjectile = Instantiate(cannonBall);
        newProjectile.transform.position += emitter.transform.position + direction * 0.5f;
        newProjectile.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);

        Vector3 leftSide = Quaternion.AngleAxis(90, Vector3.forward) * direction * 0.7f;

        var newProjectile2 = Instantiate(cannonBall);
        newProjectile2.transform.position += emitter.transform.position + direction * 0.5f + leftSide;
        newProjectile2.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);

        var newProjectile3 = Instantiate(cannonBall);
        newProjectile3.transform.position += emitter.transform.position + direction * 0.5f - leftSide;
        newProjectile3.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);
        
    }
    
}
