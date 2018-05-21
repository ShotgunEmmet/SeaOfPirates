using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public GameObject target;
    public enum ConcernType { Player, Ship };
    public ConcernType concernType;
    private GameObject concern;

    public enum TriggerType { once, multiple };
    public TriggerType triggerType = TriggerType.once;
    private bool triggered = false;

    private void Start()
    {
        if (concernType.Equals(ConcernType.Player))
        {
            concern = GameObject.Find("Player");
        }
        else if (concernType.Equals(ConcernType.Ship))
        {
            concern = GameObject.Find("Ship");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(triggerType.Equals(TriggerType.once) && !triggered)
        { 
            if(collider.gameObject.Equals(concern))
            {
                GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Show("Press <sprite=8> to Open");
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (triggerType.Equals(TriggerType.once) && !triggered)
        {
            if (collider.gameObject.Equals(concern))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    target.GetComponent<ITriggerable>().Trigger();
                    triggered = true;
                    GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Hide();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (triggerType.Equals(TriggerType.once) && !triggered)
        {
            if (collider.gameObject.Equals(concern))
            {
                GameObject.FindGameObjectWithTag("Prompt").GetComponent<Prompt>().Hide();
            }
        }
    }
}
