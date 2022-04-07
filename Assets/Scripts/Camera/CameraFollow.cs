using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    
    public Vector3 Offset = new Vector3(0, 5, 20);

    public bool EnableYMinMax = false;
    public float YMin = -200;
    public float YMax = 200;

    public bool EnableXMinMax = false;
    public float XMin = -200;
    public float XMax = 200;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + Offset;

        if (EnableYMinMax)
        {
            if (transform.position.y < YMin)
            {
                transform.position = new Vector3(transform.position.x, YMin, transform.position.z);
            }
            if (transform.position.y > YMax)
            {
                transform.position = new Vector3(transform.position.x, YMax, transform.position.z);
            }
        }

        if (EnableXMinMax)
        {
            if (transform.position.x < XMin)
            {
                transform.position = new Vector3(XMin, transform.position.y, transform.position.z);
            }
            if (transform.position.x > XMax)
            {
                transform.position = new Vector3(XMax, transform.position.y, transform.position.z);
            }
        }
    }
}
