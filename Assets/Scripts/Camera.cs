using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Camera {
    
	public static void SetFocus(GameObject subject)
    {
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(subject.transform.position.x, subject.transform.position.y, camera.transform.position.z);
        camera.transform.SetParent(subject.transform);
    }

    public static void ShowPrompt(bool status)
    {
        GameObject prompt = GameObject.Find("Prompt");
        (prompt.GetComponent("SpriteRenderer") as SpriteRenderer).enabled = status;
    }

}
