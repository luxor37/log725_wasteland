using System;
using System.Collections.Generic;
using Status;
using UnityEngine;
using static ItemController;

public class StatusManager : MonoBehaviour
{
    public Dictionary<StatusEnum, string> statusDictionary;

    public static StatusManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private StatusManager()
    {
        statusDictionary = new Dictionary<StatusEnum, string>
        {
            [StatusEnum.Fire] = "Status.FireStatus",
            [StatusEnum.Recovery] = "Status.RecoveryStatus",
            [StatusEnum.AttackBoost] = "Status.AttackBoost",
            [StatusEnum.Coin] = "Status.CoinStatus",
            [StatusEnum.Shield] = "Status.ShieldStatus",
            [StatusEnum.Wind] = "Status.WindStatus",
            [StatusEnum.FireTornado] = "Status.FireTornado",
            [StatusEnum.IsHit] = "Status.IsHitStatus"
        };
    }


    public IStatus GetNewStatusObject(StatusEnum statusName, StatusController controller)
    {
        if (statusDictionary.ContainsKey(statusName))
        {
            object[] args = { controller };
            var obj = Activator.CreateInstance(Type.GetType(statusDictionary[statusName])!, args);
            return (IStatus) obj;
        } 
        return null;
    }
}
