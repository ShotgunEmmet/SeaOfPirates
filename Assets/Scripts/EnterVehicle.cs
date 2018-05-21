using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{

    public GameObject prompt;

    public Move vehicle;

    public Transform exit;

    private float activated = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in: "+ collision.gameObject.name);
        if (collision.gameObject.name.Equals("Player"))
            Camera.ShowPrompt(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (activated < 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (collision.gameObject.name.Equals("Player"))
                {
                    activated = 1f;

                    //(this.GetComponent("SpriteRenderer") as SpriteRenderer).enabled = false;
                    
                    (collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled = false;
                    (collision.gameObject.GetComponent(typeof(Move)) as Move).SetActive(false);
                    vehicle.Controlled = true;
                    Camera.SetFocus(vehicle.gameObject);
                }
            }
        }
        else
        {
            activated -= Time.deltaTime;
        }
    }

    private void Update()
    {
        if (activated < 0) {
            if (Input.GetButtonDown("Fire1"))
            {
                if (vehicle.transform.childCount == 1)
                {
                    activated = 1f;

                    //(this.GetComponent("SpriteRenderer") as SpriteRenderer).enabled = true;
                    
                    (GameObject.Find("Player").GetComponent(typeof(Move)) as Move).Controlled = true;
                    (GameObject.Find("Player").GetComponent(typeof(Move)) as Move).SetActive(true);
                    GameObject.Find("Player").transform.position = exit.position;
                    vehicle.Controlled = false;
                    Camera.SetFocus(GameObject.Find("Player"));
                }
            }
        }
        else
        {
            activated -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("out: " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("Player"))
            Camera.ShowPrompt(false);
    }

}