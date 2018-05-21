using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour {

    private float activated = 1f;

    public Transform exit;
    public Transform anchor;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("Ship"))
            if (GameObject.FindWithTag("InputManager").GetComponent<InputManager>().controlableCharacter.Equals(collider.GetComponent<Move>()))
                GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Show("Press <sprite=8> to Dock");
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("Ship"))
        {
            if (GameObject.FindWithTag("InputManager").GetComponent<InputManager>().controlableCharacter.Equals(collider.GetComponent<Move>()))
            {
                if (activated < 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                   
                        activated = 1f;
                        
                        collider.gameObject.GetComponent<Move>().Reset(anchor.position);
                        
                        GameObject.Find("Player").GetComponent<Move>().Reset(exit.position);
                        GameObject.Find("Player").GetComponent<Move>().SetActive(true);
                        GameObject.Find("Player").transform.position = exit.position;

                        Camera.SetFocus(GameObject.Find("Player"));
                        GameObject.FindWithTag("InputManager").GetComponent<InputManager>().controller = GameObject.Find("Player").GetComponent<Move>();

                        GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Hide();
                    }
                }
                else
                {
                    activated -= Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("Ship"))
            GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Hide();
    }

}
