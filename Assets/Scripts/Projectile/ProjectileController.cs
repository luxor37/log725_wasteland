using UnityEngine;

class ProjectileController : MonoBehaviour
{
    public float duration = 1f;
    public float speed;
    public float acceleration = 0f;
    public Vector3 direction;
    public LayerMask targetLayer;
    public ParticleSystem exploseEffect;

    float timer = 0f;

    // Start is called before the first frame updated
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
            exploseEffect = Instantiate(exploseEffect, other.transform.GetChild(2).position, Quaternion.identity);
            exploseEffect.Play();
        }
        if (other.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
