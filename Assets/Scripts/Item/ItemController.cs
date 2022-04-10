using Player;
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
        FireTornado,
        IsHit
    }

    public StatusEnum StatusName;


    private void OnTriggerStay(Component other)
    {
        if (other.gameObject.tag != "Character") return;

        var controller = other.GetComponent<PlayerStatusController>();
        if (controller != null)
        {
            var status = StatusManager.Instance.GetNewStatusObject(StatusName, controller);
            controller.AddStatus(status);
        }
        Destroy(gameObject);
    }
}
