using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public List<Attack> List_PlayerAttack = new List<Attack>();
    public List<Attack> List_MinionAttack = new List<Attack>();
    public List<Attack> List_BossAttack = new List<Attack>();
    
    private static AttackManager instance = null;
    
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
    
    public static AttackManager Instance
    {
        get
        {
            return instance;
        }

    }
    
}