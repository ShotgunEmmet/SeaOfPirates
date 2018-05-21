using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour {

    private float activated = 1f;

    public Transform exit;
    public Transform anchor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
            if ((collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled)
                (GameObject.FindGameObjectWithTag("Prompt").GetComponent(typeof(Prompt)) as Prompt).Show("Press <sprite=8> to Dock");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
        {
            if ((collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled)
            {
                if (activated < 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                   
                        activated = 1f;
                        
                        (collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled = false;
                        (collision.gameObject.GetComponent(typeof(Move)) as Move).Reset(anchor.position);
                        
                        (GameObject.Find("Player").GetComponent(typeof(Move)) as Move).Controlled = true;
                        (GameObject.Find("Player").GetComponent(typeof(Move)) as Move).Reset(exit.position);
                        (GameObject.Find("Player").GetComponent(typeof(Move)) as Move).SetActive(true);
                        GameObject.Find("Player").transform.position = exit.position;

                        Camera.SetFocus(GameObject.Find("Player"));

                        (GameObject.FindGameObjectWithTag("Prompt").GetComponent(typeof(Prompt)) as Prompt).Hide();
                    }
                }
                else
                {
                    activated -= Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
            (GameObject.FindGameObjectWithTag("Prompt").GetComponent(typeof(Prompt)) as Prompt).Hide();
    }

}
