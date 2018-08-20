using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBoat : NetworkBehaviour, ITriggerable{
    public List<WayfarerInputHandler> NearbyPlayers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayerEnterTrigger(WayfarerInputHandler wayfarer)
    {
        if (isServer && !NearbyPlayers.Contains(wayfarer))
        {
            NearbyPlayers.Add(wayfarer);
            RpcEnterTrigger(wayfarer.gameObject);
        }
    }
    public void PlayerExitTrigger(WayfarerInputHandler wayfarer)
    {
        if (isServer && NearbyPlayers.Contains(wayfarer))
        {
            NearbyPlayers.Remove(wayfarer);
            RpcExitTrigger(wayfarer.gameObject);
        }
    }
    [ClientRpc]
    void RpcEnterTrigger(GameObject wayfarer)
    {
        wayfarer.GetComponent<WayfarerInputHandler>().SetNearbyTriggerable(gameObject);
    }
    [ClientRpc]
    void RpcExitTrigger(GameObject wayfarer)
    {
        wayfarer.GetComponent<WayfarerInputHandler>().LeaveNearbyTriggerable(gameObject);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
