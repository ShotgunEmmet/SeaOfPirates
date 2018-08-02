using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControls : Move, IControllable
{

    private enum ControlType { move, selection, map, menu };
    private ControlType controlType;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float angle = 0;
    public void Reset()
    {
        speed = 2f;
        controlType = ControlType.move;
    }
    public override void OnStartLocalPlayer()
    {
        //GetComponent<SpriteRenderer>().color = Color.blue;
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //transform.Rotate(0, x, 0);
        if (move.magnitude > .04f)
        {
            var deltaMove = move * Time.deltaTime * speed;
            transform.Translate(deltaMove.x, deltaMove.y, 0);
            angle = Vector3.Angle(move, transform.up);

            if (move.x > 0f)
                angle = -angle;

            angle += 180f;

            (animationManager as PlayerAnimationManager).Move(angle);
        }
        else
        {
            animationManager.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        
        Vector2 bulletDirection = (Vector2)(Quaternion.Euler(0, 0, angle - 90) * Vector2.right);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 6;
        Debug.Log("bullet direction: -" + bulletDirection);
        Debug.Log("bullet position: -" + bullet.transform.position);
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 5.0f);
    }
    public void Respond()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        if (controlType.Equals(ControlType.move))
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

                (animationManager as PlayerAnimationManager).Move(angle);
            }
            else
            {
                animationManager.Stop();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Use();
            }

            if (Input.GetButtonDown("Fire3"))
            {
                GameObject.FindObjectOfType<MiniMap>().Show();
                controlType = ControlType.map;
            }

            GameObject.FindObjectOfType<SelectionWheel>().UpdateSelection(gameObject.GetComponent<Inventory>());
        }
        else if (controlType.Equals(ControlType.map))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                GameObject.FindObjectOfType<MiniMap>().Hide();
                controlType = ControlType.move;
            }
        }

    }

    public void Use()
    {
        Inventory inventory = gameObject.GetComponent<Inventory>();
        if (inventory.selectedItem)
        {
            inventory.selectedItem.GetComponent<InventoryItem>().Use(gameObject, oldMove);
        }
    }
}
