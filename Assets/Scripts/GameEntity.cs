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

        if (onDeath != null)
            onDeath();
        Destroy(this.gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Debug.Log(currentHealth);
        if (currentHealth <= 0 && isDead == false)
        {
            Die();
        }
    }

    public virtual void TakeHeal(int heal)
    {
        currentHealth += heal;
        Debug.Log(currentHealth);
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public virtual int CalculateDamage()
    {
        return 0;
    }
}
