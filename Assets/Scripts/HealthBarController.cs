using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public HealthController healthController;
    public int smoothingScale = 50;

    Slider healthBarSlider;
    float targetHealth;
    float delta;

    // Start is called before the first frame update
    void Start()
    {
        targetHealth = healthController.GetPercentHP();
        healthBarSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        targetHealth = healthController.GetPercentHP();
        if (!Mathf.Approximately(targetHealth, healthBarSlider.value))
        {
            delta = (healthBarSlider.value - targetHealth) / smoothingScale;
            float newValue = healthBarSlider.value - delta;
            healthBarSlider.value = newValue;
        }
        
    }
}
