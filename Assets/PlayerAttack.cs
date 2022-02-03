using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    // public GameObject attackArea = default;

    // private bool attacking = false;
    // private float timeToAttack = 0.25f;
    // private float timer = 0f;

    // private int AttackIndex;

    // public List<Animation> attackAnimation;

    // private Animator _animator;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     attackArea = transform.GetChild(0).gameObject;
    //     _animator = this.gameObject.GetComponentInChildren<Animator>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetButtonDown("Fire1"))
    //     {
    //         Attack();
    //     }
    //     if (attacking)
    //     {
    //         timer += Time.deltaTime;
    //         if (timer >= timeToAttack)
    //         {
    //             attacking = false;
    //             //attackArea.SetActive(attacking);
    //             _animator.SetBool("isAttacking",attacking);
    //             timer = 0f;
    //         }
    //     }
    // }

    // private void Attack()
    // {
    //     if (AttackIndex == 1)
    //     {
    //         AttackIndex = 0;
    //     }
    //     else
    //     {
    //         AttackIndex++;
    //     }
    //     attacking = true;
    //     _animator.SetBool("isAttacking",attacking);
    //     _animator.SetInteger("AttackIndex",AttackIndex);
    //     //attackArea.SetActive(attacking);
    // }
}
