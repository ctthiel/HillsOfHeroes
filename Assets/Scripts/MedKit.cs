using UnityEngine;
public class HealthKit : MonoBehaviour
{
    public int healthAmount = 50;
    public AudioClip healthSound;
    public AudioSource healthSource;

    private void Start()
    {
        healthSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                transform.localScale = Vector3.zero;
                healthSource.PlayOneShot(healthSound);
                Destroy(gameObject, healthSound.length); // Destroys medkit object after sound ends
            }
        }
    }
}
