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

        Vector3 offset = Quaternion.AngleAxis(90, Vector3.forward) * direction * 0.7f;

        var leftCannonBall = Instantiate(cannonBall);
        leftCannonBall.transform.position += emitter.transform.position + direction * 0.5f + offset;
        leftCannonBall.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);
        leftCannonBall.GetComponent<CannonBall>().emitter = emitter;

        var middleCannonBall = Instantiate(cannonBall);
        middleCannonBall.transform.position += emitter.transform.position + direction * 0.5f;
        middleCannonBall.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);
        middleCannonBall.GetComponent<CannonBall>().emitter = emitter;

        var rightCannonBall = Instantiate(cannonBall);
        rightCannonBall.transform.position += emitter.transform.position + direction * 0.5f - offset;
        rightCannonBall.GetComponent<Rigidbody2D>().velocity = emitter.GetComponent<Rigidbody2D>().velocity + (new Vector2(direction.x, direction.y) * projectileSpeed);
        rightCannonBall.GetComponent<CannonBall>().emitter = emitter;

        middleCannonBall.GetComponent<AudioSource>().Play();
    }
    
}
