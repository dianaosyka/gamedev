using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class enemyDo : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;
    public float knockbackForce = 5f;
    public float minKnockbackForce = 1f; // Minimum knockback force when the player is very close
    public float knockbackDuration = 0.5f; // Duration of the knockback effect

    private bool isPlayerInRange = false;
    private bool isPlayerInAttackRange = false;
    private bool isKnockedBack = false;
    private bool isPlayerCloseEnough = false; // New variable to track if the player is close enough
    private bool isAttacking = false;
    private bool isDead = false; // New variable to track if the enemy is dead

    private Rigidbody rb;
    private Animator animatorChild;
    private int hp;
    public TextMeshPro hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animatorChild = GetComponentInChildren<Animator>();
        hp = 10;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Freeze Y position and X, Z rotation
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Prevents tunneling issues
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return; // Do nothing if the enemy is dead

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (isPlayerInRange == false) animatorChild.SetTrigger("Run");
            isPlayerInRange = true;
        }

        if (distanceToPlayer <= attackRange)
        {
            isPlayerInAttackRange = true;
        }
        else
        {
            isPlayerInAttackRange = false;
        }

        // Check if the player is close enough
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerCloseEnough = true;
        }

        if (isPlayerCloseEnough && isPlayerInRange && !isKnockedBack)
        {
            Vector3 lookAtPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookAtPosition); // Make the enemy face the player without rotating up and down

            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if (isDead || isKnockedBack || isAttacking) return; // Do nothing if the enemy is dead, knocked back, or attacking

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Prevent moving up
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    IEnumerator AttackPlayer()
    {
        if (isDead) yield break; // Do nothing if the enemy is dead

        isAttacking = true;
        // Implement attack logic here
        Debug.Log("Attacking player!");
        FirstPersonMovement playerMovement = player.GetComponent<FirstPersonMovement>();
        playerMovement.TriggerHitAnimation();
        animatorChild.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        // Check if the player is still within attack range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && playerMovement != null && isPlayerCloseEnough)
        {
            playerMovement.Knockback(transform.position, knockbackForce);
        }

        yield return new WaitForSeconds(0.5f); // Play attack animation for 1 second
        isAttacking = false;
        if (!isDead) animatorChild.SetTrigger("Run"); // Switch to Run animation

        yield return new WaitForSeconds(1f); // Wait for 1 second before the next attack
    }

    public void Knockback(Vector3 direction, float force)
    {
        if (isDead || isKnockedBack) return; // Do nothing if the enemy is dead or already knocked back

        StartCoroutine(ApplyKnockback(direction, force));
    }

    private IEnumerator ApplyKnockback(Vector3 direction, float force)
    {
        isKnockedBack = true;
        direction.y = 0; // Prevent knockback from affecting Y position
        rb.linearVelocity = Vector3.zero; // Reset velocity before applying knockback
        rb.AddForce(direction * force, ForceMode.Impulse);

        // Trigger the hit reaction animation
        if (animatorChild != null)
        {
            animatorChild.SetTrigger("Hit");
        }

        yield return new WaitForSeconds(knockbackDuration);

        isKnockedBack = false;
        rb.linearVelocity = Vector3.zero; // Reset velocity after knockback duration
    }

    void OnMouseDown()
    {
        FirstPersonMovement playerMovement = player.GetComponent<FirstPersonMovement>();
        playerMovement.TriggerAttackAnimation();

        if (isDead || !isPlayerInAttackRange) return; // Do nothing if the enemy is dead or not in attack range

        Debug.Log("Mouse clicked on enemy");
        
        Vector3 knockbackDirection = (transform.position - player.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float adjustedKnockbackForce = Mathf.Lerp(knockbackForce, minKnockbackForce, distanceToPlayer / attackRange);
        Knockback(knockbackDirection, adjustedKnockbackForce);
        hp--;
        hpText.text = hp + " HP";
        if (hp <= 0)
        {
            hpText.text = "";
            Die();
        }
    }

    void Die()
{
    isDead = true;
    animatorChild.SetTrigger("Die"); // Play the Die animation
    // Additional logic for when the enemy dies (e.g., dropping loot, opening doors, etc.)
    GameObject door = GameObject.FindWithTag("Door");
    if (door != null)
    {
        door.transform.Rotate(0, -90, 0);
        door.transform.position += new Vector3(0.5f, 0, 0);
    }
    StartCoroutine(DestroyAfterDelay());
}

IEnumerator DestroyAfterDelay()
{
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
}
}