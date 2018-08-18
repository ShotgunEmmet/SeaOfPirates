using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Follow();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Follow();
    }

    private void Follow()
    {
        Vector3 position = GameObject.Find("InputManager").GetComponent<WayfarerInputHandler>().transform.position;
        position.y = transform.position.y;
        transform.position = position;
    }
}
