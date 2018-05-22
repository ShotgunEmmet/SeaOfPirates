using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{

    public GameObject prompt;

    public Move vehicle;

    public Transform exit;

    private float activated = 0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("in: "+ collider.gameObject.name);
        if (collider.gameObject.name.Equals("Player"))
            GameCamera.ShowPrompt(true);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (activated < 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (collider.gameObject.name.Equals("Player"))
                {
                    activated = 1f;

                    //this.GetComponent<SpriteRenderer>().enabled = false;

                    //collider.gameObject.GetComponent<Move>().Controlled = false;
                    collider.gameObject.GetComponent<Move>().SetActive(false);
                    //vehicle.Controlled = true;
                    GameCamera.SetFocus(vehicle.gameObject);
                    GameObject.Find("InputManager").GetComponent<InputManager>().controlableCharacter = vehicle.gameObject.GetComponent<Move>();
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

                    //this.GetComponent<SpriteRenderer>().enabled = true;
                    
                    //GameObject.Find("Player").GetComponent<Move>().Controlled = true;
                    GameObject.Find("Player").GetComponent<Move>().SetActive(true);
                    GameObject.Find("Player").transform.position = exit.position;
                    //vehicle.Controlled = false;
                    GameCamera.SetFocus(GameObject.Find("Player"));
                    GameObject.Find("InputManager").GetComponent<InputManager>().controlableCharacter = GameObject.Find("Player").GetComponent<Move>();
                }
            }
        }
        else
        {
            activated -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("out: " + collider.gameObject.name);
        if (collider.gameObject.name.Equals("Player"))
            GameCamera.ShowPrompt(false);
    }

}