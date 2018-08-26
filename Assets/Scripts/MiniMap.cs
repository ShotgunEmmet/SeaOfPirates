using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {

    RawImage map;
    Camera miniMapCamera;

    private float minZoom = 10f;
    private float maxZoom = 60f;
    private float zoomSpeed = 8f;

    private void Start()
    {
        GetMap();
        GetMiniMapCamera();
        HideMap();
    }

    private void GetMap()
    {
        map = gameObject.GetComponent<RawImage>();
    }

    private void GetMiniMapCamera()
    {
        var cameraGameObject = GameObject.FindWithTag("MiniMapCamera");
        if (cameraGameObject != null)
        {
            miniMapCamera = GameObject.FindWithTag("MiniMapCamera").GetComponent<Camera>();
        }
    }

    private void Update()
    {
        UpdateMiniMap();
    }

    private void UpdateMiniMap()
    {
        if (map.enabled && miniMapCamera != null)
        {
            if (Input.GetAxis("Left_Trigger").Equals(1))
            {
                miniMapCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
                if (miniMapCamera.orthographicSize < minZoom)
                {
                    miniMapCamera.orthographicSize = minZoom;
                }
            }

            if (Input.GetAxis("Right_Trigger").Equals(1))
            {
                miniMapCamera.orthographicSize += Time.deltaTime * zoomSpeed;
                if (miniMapCamera.orthographicSize > maxZoom)
                    miniMapCamera.orthographicSize = maxZoom;
            }
        }
    }

    public void ToggleShowMap()
    {
        map.enabled = !map.enabled;
    }

    public void ShowMap()
    {
        map.enabled = true;
    }

    public void HideMap()
    {
        map.enabled = false;
    }
}
