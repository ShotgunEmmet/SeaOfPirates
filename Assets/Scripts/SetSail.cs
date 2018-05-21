using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSail : MonoBehaviour {

    private float activated = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
            if ((collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled)
                (GameObject.FindGameObjectWithTag("Prompt").GetComponent(typeof(Prompt)) as Prompt).Show("Press <sprite=8> to Board Ship");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if ((collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled)
                {
                if (activated < 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {

                        activated = 1f;
                        
                        (collision.gameObject.GetComponent(typeof(Move)) as Move).Controlled = false;
                        (collision.gameObject.GetComponent(typeof(Move)) as Move).SetActive(false);
                        
                        (GameObject.Find("Ship").GetComponent(typeof(Move)) as Move).Controlled = true;

                        Camera.SetFocus(GameObject.Find("Ship"));

                        (GameObject.FindGameObjectWithTag("Prompt").GetComponent(typeof(Prompt)) as Prompt).Show("Press <sprite=8> to Dock");
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
        if (collision.gameObject.name.Equals("Player"))
            (GameObject.FindGameObjectWithTag("Prompt").GetComponent(typeof(Prompt)) as Prompt).Hide();
    }

}