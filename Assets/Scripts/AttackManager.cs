using System.Collections.Generic;
using Player;
using Status;
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
            addPlayerAttack();
        }
    }
    
    public static AttackManager Instance
    {
        get
        {
            return instance;
        }

    }

    private void addPlayerAttack()
    {
        Attack PunchOne = new Attack(10, AttackType.Aoe, AttackForm.Melee, null,null,null,CharacterElement.Fire, new FireStatus());
        Attack PunchTwo = new Attack(30, AttackType.Aoe, AttackForm.Melee, null,null,null,CharacterElement.Fire, new FireStatus());
        Attack PunchThree = new Attack(50, AttackType.Aoe, AttackForm.Melee, null,null,null,CharacterElement.Fire, new FireStatus());
        
        List_PlayerAttack.Add(PunchOne);
        List_PlayerAttack.Add(PunchTwo);
        List_PlayerAttack.Add(PunchThree);
    }

    public  List<Attack> getPlayerAttacks(int characterIndex)
    {
        return List_PlayerAttack;
    }



}