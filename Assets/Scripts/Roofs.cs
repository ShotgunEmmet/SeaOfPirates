using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Roofs : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in: " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("Player"))
            gameObject.GetComponent<TilemapRenderer>().enabled = false;
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("out: " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("Player"))
            gameObject.GetComponent<TilemapRenderer>().enabled = true;
    }
}
