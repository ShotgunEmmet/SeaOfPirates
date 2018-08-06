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

    [SyncVar(hook = "OnChangeAngle")]
    public float angle = 0;
    public float bulletSpeed = 5f;
    public float bulletLifeTime = 5f;
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
        Respond();
        /*
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //transform.Rotate(0, x, 0);
        if (move.magnitude > .04f)
        {
            MovePlayer(move);
        }
        else
        {
            animationManager.Stop();
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    private void MovePlayer(Vector3 move)
    {
        var deltaMove = move * Time.deltaTime * speed;
        transform.Translate(deltaMove.x, deltaMove.y, 0);
        angle = Vector3.Angle(move, transform.up);

        if (move.x > 0f)
            angle = -angle;

        angle += 180f;
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,Quaternion.AngleAxis(angle, Vector3.forward));

        var bullet2D = bullet.GetComponent<Rigidbody2D>(); 
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.down);
        bullet2D.velocity = dir * bulletSpeed;

        Destroy(bullet, bulletLifeTime);

        NetworkServer.Spawn(bullet);

    }
    void OnChangeAngle(float changedAngle)
    {
        Debug.Log("RoTayto!");
        this.angle = changedAngle;
        (animationManager as PlayerAnimationManager).Move(angle);
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
                GameObject.FindObjectOfType<MiniMap>().ShowMap();
                controlType = ControlType.map;
            }

            GameObject.FindObjectOfType<SelectionWheel>().UpdateSelection(gameObject.GetComponent<Inventory>());
        }
        else if (controlType.Equals(ControlType.map))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                GameObject.FindObjectOfType<MiniMap>().HideMap();
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
