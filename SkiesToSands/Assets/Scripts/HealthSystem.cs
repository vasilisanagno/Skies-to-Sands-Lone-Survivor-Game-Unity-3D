using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Image healthBar;
    public int health = 200;
    public GameObject healthValue; // This should reference the GameObject with the TextMeshPro component
    private TextMeshProUGUI healthText;

    void Start()
    {
        // Get the TextMeshPro component from the healthValue GameObject
        healthText = healthValue.GetComponent<TextMeshProUGUI>();
        UpdateHealthText(); // Initialize the health text display
    }

    void Update()
    {
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
            healthBar.fillAmount = health / 200f;
        }
        else
        {
            healthBar.fillAmount = health / 200f;
        }
        UpdateHealthText(); // Update the text display
        Debug.Log("health: " + health);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > 200)
        {
            health = 200;
            healthBar.fillAmount = health / 200f;
        }
        else
        {
            health = Mathf.Clamp(health, 0, 200);
            healthBar.fillAmount = health / 200f;
        }
        UpdateHealthText(); // Update the text display
        Debug.Log("health: " + health);
    }

    public void SetMaxHealth() {
        health = 200;
        healthBar.fillAmount = health / 200f;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        // Update the text component to display the current health
        healthText.text = health.ToString() + "/200";
    }
}
