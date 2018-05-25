using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Move controlableCharacter;
    private IControllable controlable;

    private void Start()
    {
        controlable = controlableCharacter.GetComponent<IControllable>();
    }

    // Update is called once per frame
    void Update () {
        if (controlable != null)
        {
            controlable.Respond();
        }
	}

    public Move controller
    {
        get { return controlableCharacter; }
        set {
            controlableCharacter = value;
            controlable = controlableCharacter.GetComponent<IControllable>();
        }
    }
}
