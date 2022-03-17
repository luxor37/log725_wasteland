using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        if (cam == null)
            cam = Camera.main;
        GetComponent<Canvas>().worldCamera = cam;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       transform.LookAt(cam.transform.position); 
    }
}
