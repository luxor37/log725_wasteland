using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    Status.IStatus ItemStatus;
    public string statusName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player collided with item");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player collided with item");
            var controller = other.GetComponent<Status.StatusController>();
            ItemStatus = StatusManager.Instance.GetNewStatusObject(statusName, controller);
            controller.AddStatus(ItemStatus);
            Destroy(gameObject);
        }
    }
}
