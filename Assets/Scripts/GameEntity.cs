using System; 
using Player;
using UnityEngine;

public class GameEntity : MonoBehaviour, IDamageble
{
    public int maxHealth;
    public int currentHealth;
    protected bool isDead;

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
        Destroy(this.gameObject);
        
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
}
