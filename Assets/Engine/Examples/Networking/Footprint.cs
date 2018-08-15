using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour {

    private void Start()
    {
        GetComponent<Animator>().Play("Footprints");
    }

    void DeleteOnEnd()
    {
        GameObject.Destroy(gameObject);
    }
}
