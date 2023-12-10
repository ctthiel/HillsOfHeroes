using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int currentHealth)
    {
        slider.maxValue = currentHealth;
        slider.value = currentHealth;
    }

    public void Sethealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
