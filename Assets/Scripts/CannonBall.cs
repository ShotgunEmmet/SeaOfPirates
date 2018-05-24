using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public GameObject emitter;
    float destroyDistance = 11f;
    
    void Update()
    {
        if ((emitter.transform.position - transform.position).magnitude > destroyDistance)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
