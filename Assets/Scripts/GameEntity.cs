using System;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    protected bool IsDead;
    public event Action onDeath;
    public bool isInvincible = false;

    public GameObject floatingPoint;

    public Color damageIndicatorColor;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    //When call Die(), we trigger onDeath event
    protected void Die()
    {
        IsDead = true;

        onDeath?.Invoke();
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        if (isInvincible)
            damage = 0;
        ShowStat("-" + damage, damageIndicatorColor);
        currentHealth -= damage;
        if (currentHealth <= 0 && IsDead == false)
        {
            Die();
        }
    }

    public virtual void TakeHeal(int heal)
    {
        currentHealth += heal;

        ShowStat("+" + heal, new Color(0, 1, 0, 1));

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void ShowStat(string value, Color color, float offset = 0)
    {
        if (floatingPoint == null) return;
        floatingPoint.GetComponentInChildren<TextMesh>().color = color;
        floatingPoint.GetComponentInChildren<TextMesh>().text = value;
        Instantiate(floatingPoint, transform.position + new Vector3(0, 2f + offset, 0), Quaternion.identity);
    }
}
