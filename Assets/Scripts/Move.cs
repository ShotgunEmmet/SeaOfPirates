using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public bool Controlled = true;
    public float speed = 4f;

    Graphics graphics;

    private Vector3 oldMove = Vector3.down;

	// Use this for initialization
	void Start () {
        graphics = gameObject.GetComponent(typeof(Graphics)) as Graphics;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        if (Controlled)
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            if (move.magnitude > .04f)
            {
                oldMove = move;

                gameObject.GetComponent<Rigidbody2D>().velocity = move * speed;

                float angle = Vector3.Angle(move, transform.up);

                if (move.x > 0f)
                    angle = -angle;

                angle += 180f;

                graphics.Move(angle);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Use();
            }

            
            if (Input.GetAxis("Left_Trigger").Equals(1) && gameObject.name.Equals("Ship"))
            {
                Use();
            }

            if (Input.GetAxis("Right_Trigger").Equals(1) && gameObject.name.Equals("Ship"))
            {
                Use();
            }
        }
    }

    public void Reset(Vector3 position)
    {
        transform.position = position;
        graphics.ResetLook();
    }

    public void SetActive(bool active)
    {
        (gameObject.GetComponent("BoxCollider2D") as BoxCollider2D).enabled = active;
        (gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).enabled = active;
    }

    public void Use()
    {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        if (inventory.selectedItem)
        {
            inventory.selectedItem.GetComponent<InventoryItem>().Use(transform.position, oldMove);
        }
    }
}
