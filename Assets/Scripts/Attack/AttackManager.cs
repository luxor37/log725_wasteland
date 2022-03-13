using System.Collections.Generic;
using Player;
using Status;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public List<Attack> List_PlayerAttack = new List<Attack>();
    public List<Attack> List_EnemyAttack = new List<Attack>();
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

    public List<Attack> addPlayerAttack(int CharacterIndex)
    {
        List_PlayerAttack.Clear();
        if (CharacterIndex == 0)
        {
            Attack PunchOne = new Attack(10, 1,1,AttackType.Aoe, AttackForm.Melee,null,CharacterElement.Fire, new FireStatus());
            Attack PunchTwo = new Attack(30, 1,1,AttackType.Aoe, AttackForm.Melee, null,CharacterElement.Fire, new FireStatus());
            Attack PunchThree = new Attack(50, 1,1,AttackType.Aoe, AttackForm.Melee, null,CharacterElement.Fire, new FireStatus());
            Attack RifleShoot = new Attack(20, 4,3,AttackType.Single, AttackForm.Range, null,CharacterElement.Fire, null);
        
            List_PlayerAttack.Add(PunchOne);
            List_PlayerAttack.Add(PunchTwo);
            List_PlayerAttack.Add(PunchThree);
            List_PlayerAttack.Add(RifleShoot);
            
        }

        return List_PlayerAttack;
    }

    public  List<Attack> addEnemyAttack(int Type)
    {
        List_EnemyAttack.Clear();
        if (Type == (int) EnemyType.Zombie)
        {
            Attack ZombieAttack = new Attack(50, 1,1,AttackType.Aoe, AttackForm.Melee,null,CharacterElement.None, null);
            List_EnemyAttack.Add(ZombieAttack);
        }

        return List_EnemyAttack;
    }



}