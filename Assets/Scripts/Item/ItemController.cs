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

    private Item.BuyableItem buyComponent;

    private void Awake()
    {
        buyComponent = GetComponent<Item.BuyableItem>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character" && !buyComponent)
        {
            var controller = other.GetComponent<PlayerStatusController>();
            ApplyItem(controller);
            Destroy(gameObject);
        }
    }

    public void ApplyItem(PlayerStatusController controller)
    {
        if (controller != null)
        {
            var status = StatusManager.Instance.GetNewStatusObject(statusName, controller);
            controller.AddStatus(status);
        }
    }
}
