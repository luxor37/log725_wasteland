using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using Status;
using UnityEngine;

public enum EnemyAttackType
{
    NormalAttack, SpecialAttack
}

public enum BossAttackType
{
    NormalAttack, SpecialAttackOne
}

public class EnemyAttack : MonoBehaviour
{

    private List<Attack> allAttacks;
    private StatusHandler _Statushandler;
    private EnemyCharacter _enemyCharacter;
    private EnemyStatesController _enemyStates;
    private LayerMask playerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyCharacter = GetComponent<EnemyCharacter>();
        _enemyStates = GetComponent<EnemyStatesController>();
        playerMask = _enemyStates.parameter.layer;
        allAttacks = AttackManager.Instance.addEnemyAttack(_enemyCharacter.EnemyID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void NormalAttack()
    {
        if (_enemyCharacter.EnemyID == (int) EnemyType.Zombie)
        {
            allAttacks[(int) EnemyAttackType.NormalAttack].AttackStartPoint = _enemyStates.parameter.attackPoints;
            allAttacks[(int) EnemyAttackType.NormalAttack].DamageRadiusX = _enemyStates.parameter.attackRange;
            Hit(allAttacks[(int) EnemyAttackType.NormalAttack]);
        }
        else if (_enemyCharacter.EnemyID == (int) EnemyType.BossOne)
        {
            allAttacks[(int)BossAttackType.NormalAttack].AttackStartPoint = _enemyStates.parameter.attackPoints;
            allAttacks[(int) EnemyAttackType.NormalAttack].DamageRadiusX = _enemyStates.parameter.attackRange;
            Hit(allAttacks[(int) EnemyAttackType.NormalAttack]);
        }


    }

    private void Hit(Attack atk)
    {
        if (atk.AttackType == AttackType.Aoe)
        {
            Collider[] hitplayer =
                Physics.OverlapBox(atk.AttackStartPoint.position, new Vector3(atk.DamageRadiusX,1,1), Quaternion.identity, playerMask);
            foreach (Collider player in hitplayer)
            {
                if (atk.Debuff != null)
                {
                    atk.Debuff.AddHandler(player.GetComponent<StatusHandler>());
                    player.GetComponent<StatusHandler>().AddStatus(atk.Debuff);
                }
                player.GetComponent<PlayerCharacter>().TakeDamage(atk.BasicAttack);
            }
        }
        else if (atk.AttackType == AttackType.Single)
        {
            //TODO : use Raycast
            Ray ray = default;
            RaycastHit hitInfo;
                    
            if (atk.AttackVFX != null)
            {
                atk.AttackVFX.Emit(1);
            }
            ray.origin = atk.AttackStartPoint.position;
            ray.direction = FindObjectOfType<PlayerCharacter>().transform.position;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform != null)
                {
                    if (atk.HitVFX != null)
                    {
                        atk.HitVFX.transform.position = hitInfo.point;
                        atk.HitVFX.transform.forward = hitInfo.normal;
                        atk.HitVFX.Emit(1);
                    }
                       
                }
    
            }
        }
    }
    
    //Boss one action
    private void AttackDash()
    {
        
    }
}
