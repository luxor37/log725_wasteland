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


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            var controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Status.StatusController>();
            ItemStatus = StatusManager.Instance.GetNewStatusObject(statusName, controller);
            if (controller != null)
            {
                controller.AddStatus(ItemStatus);
            }
            Destroy(gameObject);
        }
    }
}
