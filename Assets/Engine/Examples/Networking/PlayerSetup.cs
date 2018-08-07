using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (Behaviour b in componentsToDisable)
            {
                b.enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }
    
    void onDisable()
    {
        if (sceneCamera)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
