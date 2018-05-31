using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAnimationManager : AnimationManager2D
{

    void Start()
    {
        RunAnimation("idle");
        GameObject.Find("DebugOutput").GetComponent<Text>().text = "Idle";
        
    }

    void Update()
    {
        if (Input.GetKeyUp("s"))
        {
            Stop();
            GameObject.Find("DebugOutput").GetComponent<Text>().text = "Stop";
        }

        if (Input.GetKeyUp("d"))
        {
            RunAnimation("right");
            GameObject.Find("DebugOutput").GetComponent<Text>().text = "Walk Right";
        }

        if (Input.GetKeyUp("a"))
        {
            RunAnimation("left");
            GameObject.Find("DebugOutput").GetComponent<Text>().text = "Walk Left";
        }

        if (Input.GetKeyUp("p"))
        {
            if (animationState.Equals(AnimationState.playing))
            {
                Pause();
                GameObject.Find("DebugOutput").GetComponent<Text>().text = "Pause";
            }
            else
            {
                Play();
                GameObject.Find("DebugOutput").GetComponent<Text>().text = "Play";
            }
        }

        base.Update();
    }

}
