using System; 
using Player;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public String CharacterName;
    public int maxHealth;
    [SerializeField]protected int currentHealth;
    public int basicAttack;
    public int movementSpeed;
    private Vector3 movementDirection;

    protected bool isAttacking;
    protected bool isHit = false;
    protected bool isDead = false;

    protected GameObject characterObject;

    //Action when an entity dead
    public event Action onDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    //When call Die(), we trigger onDeath event
    protected void Die()
    {
        isDead = true;

        Destroy(gameObject);
        if (onDeath != null)
            onDeath();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && isDead == false)
        {
            Die();
        }
    }

    public virtual int CalculateDamage()
    {
        return 0;
    }

    public virtual void knockBack()
    {
        if (!isHit)
        {
            isHit = true;
        }
    }

}
