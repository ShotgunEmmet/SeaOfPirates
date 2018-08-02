using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Move : NetworkBehaviour {

    public float speed = 1f;

    protected AnimationManager2D animationManager;

    protected Vector3 oldMove = Vector3.down;

    void Start()
    {
        animationManager = gameObject.GetComponent<AnimationManager2D>();
    }

    public void Reset(Vector3 position)
    {
        transform.position = position;
        animationManager.Stop();
    }

    public void SetActive(bool active)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = active;
        gameObject.GetComponent<SpriteRenderer>().enabled = active;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

}
