using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSail : MonoBehaviour {

    private float activated = 1f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("Player"))
            if (GameObject.FindWithTag("InputManager").GetComponent<InputManager>().controlableCharacter.Equals(collider.GetComponent<Move>()))
                    GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Show("Press <sprite=8> to Board Ship");
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("Player"))
        {
            if (GameObject.FindWithTag("InputManager").GetComponent<InputManager>().controlableCharacter.Equals(collider.GetComponent<Move>()))
                {
                if (activated < 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {

                        activated = 1f;
                        
                        collider.gameObject.GetComponent<Move>().SetActive(false);


                        GameCamera.SetFocus(GameObject.Find("Ship"));
                        GameObject.FindWithTag("InputManager").GetComponent<InputManager>().controller = GameObject.Find("Ship").GetComponent<Move>();

                        GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Show("Press <sprite=8> to Dock");
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
        if (collider.gameObject.name.Equals("Player"))
            GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Hide();
    }

}