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
        map = gameObject.GetComponent<RawImage>();
        miniMapCamera = GameObject.FindWithTag("MiniMapCamera").GetComponent<Camera>();

        Hide();
    }

    private void Update()
    {
        if (map.enabled)
        {
            if (Input.GetAxis("Left_Trigger").Equals(1))
            {
                miniMapCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
                if (miniMapCamera.orthographicSize < minZoom)
                    miniMapCamera.orthographicSize = minZoom;
            }

            if (Input.GetAxis("Right_Trigger").Equals(1))
            {
                miniMapCamera.orthographicSize += Time.deltaTime * zoomSpeed;
                if (miniMapCamera.orthographicSize > maxZoom)
                    miniMapCamera.orthographicSize = maxZoom;
            }
        }
    }

    public void ToggleShow()
    {
        map.enabled = !map.enabled;
    }

    public void Show()
    {
        map.enabled = true;
    }

    public void Hide()
    {
        map.enabled = false;
    }
}
