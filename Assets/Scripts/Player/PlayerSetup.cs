using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WayfarerHealth))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    public const string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
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

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<WayfarerHealth>());
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponents()
    {
        foreach (Behaviour b in componentsToDisable)
        {
            if(b!=null)
                b.enabled = false;
        }
    }
    
    void onDisable()
    {
        if (sceneCamera)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }
}
