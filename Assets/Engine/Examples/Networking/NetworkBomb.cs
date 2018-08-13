using UnityEngine;
using UnityEngine.Networking;

public class NetworkBomb : NetworkBehaviour {

    [SerializeField]
    private float lifeTime = 1f;
    private float timePassed = 0f;


    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > lifeTime)
        {
            if (GetComponent<SpriteRenderer>().enabled)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<AudioSource>().Play();
                ExplodeBomb();
            }
            else if(!GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }


    [Client]
    private void ExplodeBomb()
    {
        CmdCreateBomb();
    }

    [Command]
    private void CmdCreateBomb()
    {
        RpcDoDamage();
    }

    [ClientRpc]//not strictly necessary as the bomb damages everyone (from the servers point of view)
    private void RpcDoDamage()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if ((player.transform.position - gameObject.transform.position).magnitude < 2f)
            {
                player.GetComponent<WayfarerHealth>().RpcTakeDamage(25);
            }
        }
    }

}
