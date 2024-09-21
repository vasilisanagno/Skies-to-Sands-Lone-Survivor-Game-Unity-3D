using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public GameObject healthManager;
    private int currentHealth;
    private HealthSystem healthSystem;  // Reference to the HealthSystem component

    public GameObject gameOverCanvas;
    private ExitGame exitGame;
    private bool firstTime = true;
    private bool isAlive = true;  // To track if player is alive

    void Start()
    {
        currentHealth = maxHealth;
        
        // Get the HealthSystem component from the healthManager GameObject
        healthSystem = healthManager.GetComponent<HealthSystem>();
        Transform exitButtonTransform = gameOverCanvas.transform.Find("ExitButton");
        exitGame = exitButtonTransform.GetComponent<ExitGame>();

        // Start health reduction coroutine
        StartCoroutine(ReduceHealthOverTime());
    }

    void Update() 
    {
        if (healthSystem.health == 0 && firstTime) {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        healthSystem.Damage(damage);
        Debug.Log("Player has damaged!");
    }

    public void InitializeStatus() {
        healthSystem.SetMaxHealth();
        firstTime = true;
        isAlive = true;  // Reset the alive status
        StopCoroutine(ReduceHealthOverTime());  // Stop existing coroutine if any
        // StartCoroutine(ReduceHealthOverTime()); // Restart the health reduction coroutine
    }

    public void Die()
    {
        // Handle player death here (e.g., respawn, game over screen, etc.)
        Debug.Log("Player has died!");
        exitGame.onPlayerDeath(gameOverCanvas);
        firstTime = false;
        isAlive = false;  // Mark player as dead to stop health reduction
    }

    // Coroutine to reduce health over time
    private IEnumerator ReduceHealthOverTime()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(20f);  // Wait for 20 seconds
            if (isAlive && healthSystem.health > 0)
            {
                TakeDamage(2);  // Reduce health by 2
                Debug.Log("Player has lost 2 health points due to time.");
            }
        }
    }
}