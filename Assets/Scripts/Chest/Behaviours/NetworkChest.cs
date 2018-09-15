using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkChest : NetworkBehaviour, ITriggerable {

    [SerializeField]
    private int layer =0;
    public int Layer { get { return layer; } }

    [SerializeField]
    private GameObject prompt;

    public List<WayfarerInputHandler> NearbyPlayers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayerEnterTrigger(WayfarerInputHandler wayfarer){
        if (isServer && !NearbyPlayers.Contains(wayfarer))
        {
            NearbyPlayers.Add(wayfarer);
            RpcEnterTrigger(wayfarer.gameObject);
        }
    }
    public void PlayerExitTrigger(WayfarerInputHandler wayfarer){
        if (isServer && NearbyPlayers.Contains(wayfarer))
        {
            NearbyPlayers.Remove(wayfarer);
            RpcExitTrigger(wayfarer.gameObject);
        }
    }
    public void Activate(GameObject triggerObject)
    {
        RpcActivate();
    }
    [ClientRpc]
    public void RpcActivate()
    {
        if (!GetComponent<Animator>().GetBool("Open"))
        {
            GetComponent<Animator>().SetBool("Open", true);
            GetComponent<AudioSource>().Play();
            if (prompt)
            {
                prompt.SetActive(false);
            }
        }
    }

    [ClientRpc]
    void RpcEnterTrigger(GameObject wayfarer)
    {
        wayfarer.GetComponent<WayfarerInputHandler>().SetNearbyTriggerable(layer, gameObject);
        if (prompt && wayfarer.GetComponent<NetworkBehaviour>().isLocalPlayer)
        {
            prompt.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    [ClientRpc]
    void RpcExitTrigger(GameObject wayfarer)
    {
        wayfarer.GetComponent<WayfarerInputHandler>().LeaveNearbyTriggerable(gameObject);
        if (prompt && wayfarer.GetComponent<NetworkBehaviour>().isLocalPlayer)
        {
            prompt.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

}
