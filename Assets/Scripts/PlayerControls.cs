using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : Move, IControllable {
    
    public void Reset()
    {
        speed = 2f;
    }

    public void Respond()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        
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
