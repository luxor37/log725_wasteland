using Player;
using Status;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public enum StatusEnum
    {
        Fire,
        Recovery,
        AttackBoost,
        Coin,
        Shield,
        Wind,
        FireTornado
    }

    public StatusEnum statusName;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            var controller = other.GetComponent<PlayerStatusController>();
            if (controller != null)
            {
                var status = StatusManager.Instance.GetNewStatusObject(statusName, controller);
                controller.AddStatus(status);
            }
            Destroy(gameObject);
        }
    }
}
