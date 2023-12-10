using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioClip hurtSound;
    public AudioSource hurtSource;
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Function to handle player taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.Sethealth(currentHealth); // Updates health bar
        hurtSource.PlayOneShot(hurtSound);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        // Increase current health by the heal amount
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

        // Updates the health bar
        healthBar.Sethealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player defeated!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
