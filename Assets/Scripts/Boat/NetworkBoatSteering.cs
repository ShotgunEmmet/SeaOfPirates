using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBoatSteering : NetworkBehaviour, IMotor, ITriggerable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    [SerializeField]
    private int layer = 0;
    private enum ControlType { move, selection, map, menu };
    private ControlType controlType;
    [SerializeField]
    private float speed = 2.4f;
    public List<WayfarerInputHandler> NearbyPlayers;
    Vector2 oldMove;
    public GameObject boat;
    WayfarerInputHandler wayfarerInputHandler;
    int ITriggerable.Layer
    {
        get
        {
            return layer;
        }
    }

    public void Reset()
    {
        speed = 2f;
        controlType = ControlType.move;
    }
    [Command]
    private void CmdMoveBoat(Vector2 move)
    {
        if (move.magnitude > .04f)
        {
            oldMove = move;
            boat.GetComponent<Rigidbody2D>().velocity = move * speed;

            float angle = Vector3.Angle(move, transform.up);

            if (move.x > 0f)
                angle = -angle;

            angle += 180f;

        }
    }

    public void Move(Vector2 move)
    {
        CmdMoveBoat(move);
    }

    GameObject ITriggerable.GetGameObject()
    {
        return this.gameObject;
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
    void RpcActivate(GameObject triggerObject)
    {
        wayfarerInputHandler = triggerObject.GetComponent<WayfarerInputHandler>();
        wayfarerInputHandler.Motor = this;
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


    public void Activate(GameObject triggerObject)
    {
        RpcActivate(triggerObject);
       
    }
}





