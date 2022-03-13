using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    Status.IStatus ItemStatus;
    public string statusName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var controller = other.GetComponent<Status.StatusHandler>();
            ItemStatus = StatusManager.Instance.GetNewStatusObject(statusName, controller);
            if (controller != null)
            {
                controller.AddStatus(ItemStatus);
            }
            Destroy(gameObject);
        }
    }
}
