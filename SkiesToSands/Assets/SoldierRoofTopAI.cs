using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRoofTopAI : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float detectionRange = 30f;  // The range at which the soldier detects the player
    public float shootingRange = 30f;  // The range at which the soldier starts shooting
    public float shootingInterval = 1.3f;  // Time interval between shots
    public int damage = 10;  // Damage dealt to the player per shot
    public float hitChance = 0.5f;  // Chance that a bullet will hit the player (50%)

    private bool isShooting = false;

    private Animator animator;  // Reference to the Animator component

    void Start()
    {
        animator = GetComponent<Animator>();  // Get the Animator component on this GameObject
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Look at the player
            LookAtPlayer();

            if (distanceToPlayer <= shootingRange)
            {
                // Start attack animation and shooting if not already shooting
                if (!isShooting)
                {
                    StartShooting();
                }
            }
            else
            {
                // Stop shooting and reset attack animation if player is out of shooting range
                StopShooting();
            }
        }
        else
        {
            // Stop shooting and reset attack animation if player is out of detection range
            StopShooting();
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // Smooth rotation towards the player
    }

    void StartShooting()
    {
        isShooting = true;
        animator.SetBool("Attack", true);  // Start the attack animation (which loops)
        StartCoroutine(ShootAtPlayer());
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("Attack", false);  // Stop the attack animation
        StopCoroutine(ShootAtPlayer());  // Stop dealing damage
    }

    IEnumerator ShootAtPlayer()
    {
        while (isShooting)
        {
            if (Random.value <= hitChance)
            {
                // Simulate shooting by applying damage directly to the player
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
            }

            // Wait for the next shot
            yield return new WaitForSeconds(shootingInterval);
        }
    }
}