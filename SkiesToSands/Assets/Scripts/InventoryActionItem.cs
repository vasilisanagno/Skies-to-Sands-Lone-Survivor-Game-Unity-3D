using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class InventoryItemAction : MonoBehaviour
{
    public float healAmount = 10f; // Amount to heal
    private HealthSystem healthSystem;
    public Item item;

    private GameObject weapon;
    private GameObject player;
    private GameObject mainCamera;
    private GameObject fps;
    private GameObject ui;
    private GameObject reticle;

    private Image notificationText; // Reference to the TextMeshPro UI element for showing messages
    private Image tomatoSoupText;
    private Image redPotionText;

    private NotificationManager notificationManager;
    

    void Start()
    {
        // Find the HealthSystem in the scene
        healthSystem = FindObjectOfType<HealthSystem>();

        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem not found in the scene.");
        }
        notificationManager = FindObjectOfType<NotificationManager>();
    }

    public void OnItemClicked()
    {
        if (healthSystem != null)
        {
            if(item.itemName == "Red Potion"){
                if (healthSystem.health == 200) {
                    notificationText = GameObject.Find("HealthMaximumCanvas").gameObject.transform
                    .Find("HealthMaximumImage").GetComponent<Image>();
                    notificationManager.ShowMessage(notificationText);
                    return;
                }
                else {
                    redPotionText = GameObject.Find("PotionCanvas").gameObject.transform
                    .Find("PotionImage").GetComponent<Image>();
                    notificationManager.ShowMessage(redPotionText);
                    healthSystem.Heal(20);
                    Debug.Log("Healed 20 health points.");
                }
            }
            else if(item.itemName == "Tomato Soup"){
                if (healthSystem.health == 200) {
                    notificationText = GameObject.Find("HealthMaximumCanvas").gameObject.transform
                    .Find("HealthMaximumImage").GetComponent<Image>();
                    notificationManager.ShowMessage(notificationText);
                    return;
                }
                else {
                    tomatoSoupText = GameObject.Find("TomatoSoupCanvas").gameObject.transform
                    .Find("TomatoSoupImage").GetComponent<Image>();
                    notificationManager.ShowMessage(tomatoSoupText);
                    healthSystem.Heal(10);
                    Debug.Log("Healed 10 health points.");
                }
            }
            else if(item.itemName == "Weapon") {
                player = GameObject.Find("PlayerCapsuleOG");
                mainCamera = player.transform.Find("MainCamera").gameObject;
                fps = mainCamera.transform.Find("First Person Camera").gameObject;
                weapon = fps.transform.Find("Carbine").gameObject;
                ui = GameObject.Find("UI");
                reticle = ui.transform.Find("Gun Reticle Canvas").gameObject;
                weapon.SetActive(true);
                reticle.SetActive(true);
                Debug.Log("Equipped the weapon.");
            }
            else if(item.itemName == "Wolf Meat") {
                if (healthSystem.health == 200) {
                    notificationText = GameObject.Find("HealthMaximumCanvas").gameObject.transform
                    .Find("HealthMaximumImage").GetComponent<Image>();
                    notificationManager.ShowMessage(notificationText);
                    return;
                }
                else {
                    healthSystem.Heal(25);
                    Debug.Log("Healed 25 health points.");
                }
            }
            else if(item.itemName == "Rabbit Meat") {
                if (healthSystem.health == 200) {
                    notificationText = GameObject.Find("HealthMaximumCanvas").gameObject.transform
                    .Find("HealthMaximumImage").GetComponent<Image>();
                    notificationManager.ShowMessage(notificationText);
                    return;
                }
                else {
                    healthSystem.Heal(15);
                    Debug.Log("Healed 15 health points.");
                }
            }

            InventoryManager.Instance.Remove(item);

            Destroy(gameObject);
        }
    }
}
