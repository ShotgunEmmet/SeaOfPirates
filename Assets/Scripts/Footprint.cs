using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour {

    void DeleteOnEnd()
    {
        GameObject.Destroy(gameObject);
    }
}
