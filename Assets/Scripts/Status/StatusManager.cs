using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public Dictionary<string, string> statusDictionary;

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
        statusDictionary = new Dictionary<string, string>();
        statusDictionary["Fire"] = "Status.FireStatus";
        statusDictionary["Recovery"] = "Status.RecoveryStatus";
        statusDictionary["AttackBoost"] = "Status.AttackBoost";
    }


    public Status.IStatus GetNewStatusObject(string statusName, Status.StatusHandler handler)
    {
        if (statusDictionary.ContainsKey(statusName))
        {
            // var obj = Activator.CreateComInstanceFrom(Assembly.GetEntryAssembly().CodeBase, statusDictionary[statusName], controller);
            System.Object[] args = { handler };
            var obj = Activator.CreateInstance(Type.GetType(statusDictionary[statusName]), args);
            return (Status.IStatus) obj;
        } else
        {
            return null;
        }
    }
}
