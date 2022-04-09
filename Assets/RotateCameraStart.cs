using UnityEngine;

public class RotateCameraStart : MonoBehaviour
{
    public float RotationSpeed = 10;
    void Update()
    {
        var newY = transform.eulerAngles.y + RotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z));
    }
}
