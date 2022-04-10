using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider HealthBar;
    public Gradient Gradient;
    public Image GradientHp;
    private Status.StatusController _enemyStatusController;
    
    void Start()
    {
        _enemyStatusController = gameObject.GetComponent<Status.StatusController>();
        HealthBar.maxValue = _enemyStatusController.maxHealth;
        HealthBar.value = _enemyStatusController.currentHealth;
    }

    void Update()
    {
        HealthBar.value = _enemyStatusController.currentHealth;
        GradientHp.color = Gradient.Evaluate(HealthBar.normalizedValue);
    }
}
