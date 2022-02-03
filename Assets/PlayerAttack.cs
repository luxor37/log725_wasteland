using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackArea = default;

    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private int AttackIndex;

    public List<Animation> attackAnimation;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                attacking = false;
                //attackArea.SetActive(attacking);
                this.gameObject.GetComponent<Animator>().SetBool("isAttacking",attacking);
                timer = 0f;
            }
        }
    }

    private void Attack()
    {
        if (AttackIndex == 1)
        {
            AttackIndex = 0;
        }
        else
        {
            AttackIndex++;
        }
        attacking = true;
        this.gameObject.GetComponent<Animator>().SetBool("isAttacking",attacking);
        this.gameObject.GetComponent<Animator>().SetInteger("AttackIndex",AttackIndex);
        //attackArea.SetActive(attacking);
    }
}
