using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour{

    public GameObject bombPrefab;
    
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
        injuredPlayer.TakeDamage(10);
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
}
