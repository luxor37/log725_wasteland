using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    public float moveSpeed = 0.125f;
    public Vector3 offset;

    public bool enableYMinMax = false;
    public float yMin = 100;
    public float yMax = 100;

    public bool enableXMinMax = false;
    public float xMin = -100;
    public float xMax = 100;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
        else{
            Debug.Log("CameraFollow target is null!");
        }

        if (enableYMinMax)
        {
            if (transform.position.y < yMin)
            {
                transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
            }
            if (transform.position.y > yMax)
            {
                transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
            }
        }

        if (enableXMinMax)
        {
            if (transform.position.x < xMin)
            {
                transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xMax)
            {
                transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
            }
        }
    }
}
