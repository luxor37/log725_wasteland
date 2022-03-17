using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    void Awake(){
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform); 
    }
}
