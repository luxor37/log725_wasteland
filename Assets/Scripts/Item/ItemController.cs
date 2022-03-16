using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public enum StatusEnum
    {
        Fire,
        Recovery,
        AttackBoost,
        Coin
    }

    Status.IStatus ItemStatus;
    public StatusEnum statusName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player collided with item");
            var controller = other.GetComponent<Status.StatusController>();
            ItemStatus = StatusManager.Instance.GetNewStatusObject(statusName, controller);
            if (controller != null)
            {
                controller.AddStatus(ItemStatus);
            }
            Destroy(gameObject);
        }
    }
}
