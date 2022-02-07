using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 1;
    
    // public temporairement pour tester
    public float currentHealth;
    bool IsInvincible = false;

    public bool IsDead()
    {
        return currentHealth <= 0f;
    }

    public void SetInvincible(bool invincible)
    {
        IsInvincible = invincible;
    }

    public void TakeDamage(float damage)
    {
        if (!IsInvincible)
            currentHealth -= damage;
    }

    public float GetPercentHP()
    {
        return Mathf.Max(0, currentHealth / maxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
            gameObject.SetActive(false);
    }
}
