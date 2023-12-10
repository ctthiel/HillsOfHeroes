using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;

    private SkinnedMeshRenderer enemyRenderer;
    private Material originalMaterial;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (enemyRenderer != null)
        {
            originalMaterial = new Material(enemyRenderer.material);
        }
    }

    // Handles enemy taking damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        StartCoroutine(FlashRedEffect()); // Flashes red when hit

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material = new Material(originalMaterial);
        }
        Destroy(gameObject);
    }

    // Coroutine for the flash red effect 
    IEnumerator FlashRedEffect()
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = Color.red;

            yield return new WaitForSeconds(0.15f);

            enemyRenderer.material = new Material(originalMaterial);
        }
    }
}
