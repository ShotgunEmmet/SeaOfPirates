using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTrigger : MonoBehaviour {

    ITriggerable triggerable;
	// Use this for initialization
	void Start () {
        triggerable = GetComponentInParent<ITriggerable>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var wayfarer = collision.gameObject.GetComponent<WayfarerInputHandler>();
        if(wayfarer!=null){
            triggerable.PlayerEnterTrigger(wayfarer);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var wayfarer = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var wayfarer = collision.gameObject.GetComponent<WayfarerInputHandler>();
        if (wayfarer != null)
        {
            triggerable.PlayerExitTrigger(wayfarer);
        }

    }

}
