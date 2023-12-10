using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public float attackDamage = 20f;
    public float attackCooldown = 1f;
    public float attackRange = 2f;
    public float knockbackForce = 15f;
    public float knockbackDuration = 0.5f;
    public Image crosshair;

    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioSource attackSource1;
    public AudioSource attackSource2;    

    private float lastAttackTime = 0f;
    private Animator animator;
    private Animator swordAnimator;

    private void Start()
    {
        animator = GetComponent<Animator>();  
        swordAnimator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
        {
            Attack();
            swordAnimator.SetBool("SwordIdle", false);   
        }
        else
        {
            swordAnimator.SetBool("SwordIdle", true);
        }

        UpdateCrosshairPosition(); // Keeps crosshair at center of screen
    }

    // Checks if player is aiming at enemy
    bool IsAimingAtSkeleton()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {  
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    return true;
                }
            }
        } 
     
        return false;
    }

    void Attack()
    {
        Debug.Log("Player attacks");

        animator.SetTrigger("AttackTrigger");
        swordAnimator.SetTrigger("SwordAttack");
        attackSource2.PlayOneShot(attackSound2);

        RaycastHit hit;
        // Checks if player's attack hits enemy
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    attackSource1.PlayOneShot(attackSound1);
                    enemyHealth.TakeDamage(attackDamage);
                    ApplyKnockback(enemyHealth.transform);
                }
            }
        }

        lastAttackTime = Time.time;
    }

    void ApplyKnockback(Transform enemyTransform)
    {
        // Adds a force opposite direction of the player's attack
        Vector3 knockbackDirection = (enemyTransform.position - transform.position).normalized;
        enemyTransform.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    void UpdateCrosshairPosition()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        crosshair.transform.position = screenCenter;
    }
}
