using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTrigger : MonoBehaviour {
    [SerializeField]
    public NetworkChest chest;
	// Use this for initialization
	void Start () {
        chest = this.GetComponentInParent<NetworkChest>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var wayfarer = collider.gameObject.GetComponent<WayfarerInputHandler>();
        if(wayfarer!=null){
            chest.PlayerEnteredTrigger(wayfarer);
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        var wayfarer = collider.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var wayfarer = collider.gameObject.GetComponent<WayfarerInputHandler>();
        if (wayfarer != null)
        {
            chest.PlayerLeftTrigger(wayfarer);
        }

    }

}
