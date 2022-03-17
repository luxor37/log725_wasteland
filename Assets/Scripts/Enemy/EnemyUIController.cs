using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider healthBar;
    public Gradient _Gradient;
    public Image GradientHP;
    private Status.StatusController _enemyStatusController;
    
    void Start()
    {
        _enemyStatusController = this.gameObject.GetComponent<Status.StatusController>();
        healthBar.maxValue = _enemyStatusController.maxHealth;
        healthBar.value = _enemyStatusController.getCurrentHealth();
    }

    void Update()
    {
        healthBar.value = _enemyStatusController.getCurrentHealth();
        GradientHP.color = _Gradient.Evaluate(healthBar.normalizedValue);
    }
}
