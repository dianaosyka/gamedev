using System;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody rigidbody;
    private Animator animator;
    public GameObject enemyObject;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
            animator.SetTrigger("Run");
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }

    // Method to trigger the hit animation
    public void TriggerHitAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
    }
    public void TriggerAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    internal void Knockback(Vector3 forward, float knockbackForce)
    {
        
        if (enemyObject != null)
        {
            Vector3 enemyPosition = enemyObject.transform.position;
            Vector3 playerPosition = gameObject.transform.position;
            Vector3 direction = (playerPosition - enemyPosition).normalized; // Calculate the direction from the enemy to the player
            direction.y = 0; // Ensure movement is only in the x and z directions
            rigidbody.AddForce(direction * knockbackForce/100, ForceMode.Impulse); // Apply force to the player's Rigidbody
        }
    }
}