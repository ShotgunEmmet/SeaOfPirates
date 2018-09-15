using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkShipBoarding : NetworkBehaviour, ITriggerable
{
    [SerializeField]
    private int layer = 0;
    public int Layer { get { return layer; } }

    public List<WayfarerInputHandler> NearbyPlayers;

    [SerializeField]
    private BoadInterior interior;

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
    public void Activate(GameObject triggerObject)
    {
        RpcActivate(triggerObject);
    }
    [ClientRpc]
    void RpcActivate(GameObject triggerObject)
    {
        triggerObject.transform.SetPositionAndRotation(interior.transform.position, triggerObject.transform.rotation);
        triggerObject.transform.SetParent(interior.transform);

        if (triggerObject.gameObject.GetComponent<WayfarerInputHandler>().isLocalPlayer)
        {
            GameObject.FindGameObjectWithTag("ShipInterior").GetComponent<RawImage>().enabled = true;
            //GameObject playerCamera = triggerObject.gameObject.GetComponentInChildren<Camera>().gameObject;
            GameObject playerCamera = triggerObject.GetComponentInChildren<WayfarerVisuals>().playerCamera.gameObject;
            playerCamera.transform.SetParent(transform);
            playerCamera.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z), Quaternion.identity);
        }
    }

    [ClientRpc]
    void RpcEnterTrigger(GameObject wayfarer)
    {
        wayfarer.GetComponent<WayfarerInputHandler>().SetNearbyTriggerable(layer, gameObject);
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
