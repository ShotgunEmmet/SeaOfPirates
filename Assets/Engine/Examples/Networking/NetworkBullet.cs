using UnityEngine;
using UnityEngine.Networking;


public class NetworkBullet : NetworkBehaviour
{

    [SerializeField]
    private float force = 4f;
    [SerializeField]
    private float lifeTime = 3f;
    private float timePassed = 0f;

    private string playerWhoShot;

    public void Setup(Vector2 direction, string playerName)
    {
        playerWhoShot = playerName;
        GetComponent<Rigidbody2D>().AddForce(direction.normalized * force);
        gameObject.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > lifeTime)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !collision.name.Equals(playerWhoShot))
        {
            ImpactBullet(collision.name);
        }
    }

    [Client]
    private void ImpactBullet(string playerName)
    {
        CmdImpactBullet(playerName);
    }

    [Command]
    private void CmdImpactBullet(string playerName)
    {
        RpcDoDamage(playerName);
    }

    [ClientRpc]//this causes an error in the editor but its the only way it works
    private void RpcDoDamage(string playerName)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.GetPlayer(playerName).RpcTakeDamage(10);
    }
}
