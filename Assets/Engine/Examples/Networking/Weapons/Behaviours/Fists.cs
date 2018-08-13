using UnityEngine;
using System.Collections;
using Application;

public class Fists : MonoBehaviour, IWeapon
{
    public GameObject Fire(GameObject ammoPrefab)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != gameObject)
            {
                if ((player.transform.position - gameObject.transform.position).magnitude < 1f)
                {
                    ApplyDamageToPlayer(player.name);
                }
            }
        }
        return null;
    }

    private void ApplyDamageToPlayer(string playerID)
    {
        WayfarerHealth injuredPlayer = GameManager.GetPlayer(playerID);
        injuredPlayer.RpcTakeDamage(10);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
