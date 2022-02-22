using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ProjectileController : MonoBehaviour
{
    public List<string> statuses;
    public string name;
    public float duration = 1f;
    public float speed;
    public float acceleration = 0f;
    public Vector3 direction;
    public LayerMask targetLayer;

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
        speed = Mathf.Max(0f, speed + acceleration * Time.deltaTime);
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
       
       if (targetLayer.value == (targetLayer.value | (1 << other.gameObject.layer)))
        {
            
            other.GetComponent<Status.StatusController>().AddStatus(new Status.FireStatus(other.GetComponent<Status.StatusController>()));
        }
        if (other.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
