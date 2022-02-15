using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider healthBar;
    public Gradient _Gradient;
    public Image GradientHP;

    private EnemyCharacter _enemyCharacter;

    // Start is called before the first frame update
    void Start()
    {
        _enemyCharacter = this.gameObject.GetComponent<EnemyCharacter>();
        healthBar.maxValue = _enemyCharacter.maxHealth;
        healthBar.value = _enemyCharacter.CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = _enemyCharacter.CurrentHealth;
        GradientHP.color = _Gradient.Evaluate(healthBar.normalizedValue);
    }
}
