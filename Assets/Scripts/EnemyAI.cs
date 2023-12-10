using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int attackDamage = 5;
    public float attackCooldown = 2f;
    public float delayTime = 1;
    public float moveSpeed = 5f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.ResetTrigger("isIdle");
        animator.ResetTrigger("isChasing");
        agent.speed = moveSpeed;
    }

    void Update()
    {
        // Calculates distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer < attackRange && Time.time - lastAttackTime > attackCooldown)
            {
                AttackPlayer(); // Attack player if within attack range and cooldown period
            }
            else
            {
                // Handles enemy animation states
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                {
                    animator.SetBool("IsAttacking", false);
                }

                animator.SetTrigger("isChasing");
            }
        }
        else
        {
            animator.SetTrigger("isIdle"); // Go idle if player is out of detection range
            animator.ResetTrigger("isChasing");
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        StartCoroutine(DelayedDamage(attackDamage, playerHealth));

        // Triggers attack animations and updates last attack time
        animator.SetTrigger("AttackTrigger"); 
        animator.SetBool("IsAttacking", true);

        lastAttackTime = Time.time;
    }

    // Delay on player taking damage so that the animation lines up haha
    IEnumerator DelayedDamage(int damage, PlayerHealth playerHealth)
    {
        yield return new WaitForSeconds(delayTime);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
