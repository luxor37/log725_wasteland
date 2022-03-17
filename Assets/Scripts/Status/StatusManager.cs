using System;
using System.Collections.Generic;
using UnityEngine;
using static ItemController;

public class StatusManager : MonoBehaviour
{
    public Dictionary<StatusEnum, string> statusDictionary;

    private static StatusManager instance = null;

    public static StatusManager Instance
    {
        get
        {
            return instance;
        }
     
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private StatusManager()
    {
        statusDictionary = new Dictionary<StatusEnum, string>();
        statusDictionary[StatusEnum.Fire] = "Status.FireStatus";
        statusDictionary[StatusEnum.Recovery] = "Status.RecoveryStatus";
        statusDictionary[StatusEnum.AttackBoost] = "Status.AttackBoost";
        statusDictionary[StatusEnum.Coin] = "Status.CoinStatus";
    }


    public Status.IStatus GetNewStatusObject(StatusEnum statusName, Status.StatusController controller)
    {
        if (statusDictionary.ContainsKey(statusName))
        {
            System.Object[] args = { controller };
            var obj = Activator.CreateInstance(Type.GetType(statusDictionary[statusName]), args);
            return (Status.IStatus) obj;
        } else
        {
            return null;
        }
    }
}
