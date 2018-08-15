using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayfarerVisuals : MonoBehaviour {

    public GameObject trail;
    
    private Vector3 oldPos = new Vector3();

    private void Start () {
        oldPos = transform.position;
    }
	
	private void Update () {
		if((transform.position - oldPos).magnitude > 0.5f)
        {
            GameObject newTrail = Instantiate<GameObject>(trail, trail.transform.position + oldPos, Quaternion.identity);
            newTrail.GetComponent<Animator>().SetFloat("FaceX", GetComponent<Animator>().GetFloat("FaceX"));
            newTrail.GetComponent<Animator>().SetFloat("FaceY", GetComponent<Animator>().GetFloat("FaceY"));
            oldPos = transform.position;
        }
	}
}
