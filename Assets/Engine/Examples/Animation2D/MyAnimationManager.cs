using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAnimationManager : AnimationManager2D
{

    void Update()
    {

        if (Input.GetKeyUp("s"))
        {
            currentAnimation.Stop();
            GameObject.Find("DebugOutput").GetComponent<Text>().text = "Stop";
        }

        if (Input.GetKeyUp("d"))
        {
            RunAnimation("Walk_R");
            GameObject.Find("DebugOutput").GetComponent<Text>().text = "Walk Right";
        }

        if (Input.GetKeyUp("a"))
        {
            RunAnimation("Walk_L");
            GameObject.Find("DebugOutput").GetComponent<Text>().text = "Walk Left";
        }

        if (Input.GetKeyUp("p"))
        {
            if (currentAnimation.State().Equals(Animation2D.AnimationState.playing))
            {
                currentAnimation.Pause();
                GameObject.Find("DebugOutput").GetComponent<Text>().text = "Pause";
            }
            else
            {
                currentAnimation.Play();
                GameObject.Find("DebugOutput").GetComponent<Text>().text = "Play";
            }
        }
    }

}
