using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    
    public float speed = 1f;

    protected Graphics graphics;

    protected Vector3 oldMove = Vector3.down;

    void Start () {
        graphics = gameObject.GetComponent<Graphics>();
    }

    public void Reset(Vector3 position)
    {
        transform.position = position;
        graphics.ResetLook();
    }

    public void SetActive(bool active)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = active;
        gameObject.GetComponent<SpriteRenderer>().enabled = active;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    
}
