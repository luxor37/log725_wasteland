using System;
using Enemy;
using Status;
using UnityEngine;
using static ItemController;

class ProjectileController : MonoBehaviour
{
    public float duration = 1f;
    public float speed;
    public float acceleration = 0f;

    public int damage = 50;

    public Vector3 direction;
    public LayerMask targetLayer;
    public ParticleSystem exploseEffect;

    public StatusEnum appliedStatus = StatusEnum.Fire;

    float timer = 0f;

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
            var statusController = other.GetComponent<StatusController>();
            statusController.TakeDamage(damage);
            var newStatus = StatusManager.Instance.GetNewStatusObject(appliedStatus, statusController);
            other.GetComponent<StatusController>().AddStatus(newStatus);
            var explosionPos = new Vector3(other.transform.position.x, other.transform.position.y+1, other.transform.position.z);
            exploseEffect = Instantiate(exploseEffect, explosionPos, Quaternion.identity);
            exploseEffect.Play();
        }
        if (other.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
