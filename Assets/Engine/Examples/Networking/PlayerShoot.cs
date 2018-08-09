using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour{

    public GameObject bombPrefab;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlantBomb();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Punch();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            ShootBullet(gameObject.name);
        }
    }

    [Client]
    private void Punch()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != gameObject)
            {
                if ((player.transform.position - gameObject.transform.position).magnitude < 1f)
                {
                    CmdPlayerHurt(player.name);
                }
            }
        }
    }

    [Command]
    private void CmdPlayerHurt(string playerID)
    {
        Player injuredPlayer = GameManager.GetPlayer(playerID);
        injuredPlayer.RpcTakeDamage(10);
    }

    [Client]
    private void PlantBomb()
    {
        CmdCreateBomb();
    }

    [Command]
    private void CmdCreateBomb()
    {
        CreateBomb();
    }

    private void CreateBomb()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 offset = rb.velocity.normalized * 2f;//blank for the host
        
        var go = (GameObject)Instantiate(bombPrefab, transform.position + new Vector3(offset.x, offset.y, 0), Quaternion.identity);
        NetworkServer.Spawn(go);
    }


    [Client]
    private void ShootBullet(string playerName)
    {
        CmdShootBullet(playerName);
    }

    [Command]
    private void CmdShootBullet(string playerName)
    {
        CreateBullet(playerName);
    }

    private void CreateBullet(string playerName)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 offset = rb.velocity.normalized;//blank for the host

        var go = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        go.GetComponent<NetworkBullet>().Setup(new Vector3(offset.x, offset.y, 0), playerName);
        NetworkServer.Spawn(go);
    }
}
