using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the Inventory GameObject
    public InventoryManager inventoryManager; // Reference to your InventoryManager script

    public Vector2 hotSpot = Vector2.zero;
    public Texture2D customCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public FirstPersonController fps;
    public PlayerInput input;
    public Weapon weapon;

    private Image notificationText; // Reference to the TextMeshPro UI element for showing messages
    private Image tomatoSoupText;
    private Image redPotionText;

    private GameObject ui;
    private GameObject reticle;
    private GameObject carbine;
    private GameObject player;
    private GameObject mainCamera;
    private GameObject fpc;

    void Start()
    {
        // Ensure the inventory starts as inactive (closed)
        inventoryPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Listen for the key press to toggle the inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        if (inventoryPanel.activeSelf)
        {
            // If the inventory is already active, deactivate it (close it)
            inventoryPanel.SetActive(false);
            fps.enabled = true;
            input.enabled = true;
            weapon.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            notificationText = GameObject.Find("HealthMaximumCanvas").gameObject.transform
            .Find("HealthMaximumImage").GetComponent<Image>();
            notificationText.gameObject.SetActive(false); // Hide the text
            redPotionText = GameObject.Find("PotionCanvas").gameObject.transform
            .Find("PotionImage").GetComponent<Image>();
            redPotionText.gameObject.SetActive(false);
            tomatoSoupText = GameObject.Find("TomatoSoupCanvas").gameObject.transform
            .Find("TomatoSoupImage").GetComponent<Image>();
            tomatoSoupText.gameObject.SetActive(false);
            player = GameObject.Find("PlayerCapsuleOG");
            mainCamera = player.transform.Find("MainCamera").gameObject;
            fpc = mainCamera.transform.Find("First Person Camera").gameObject;
            carbine = fpc.transform.Find("Carbine").gameObject;
            if (carbine.activeInHierarchy) {
                ui = GameObject.Find("UI");
                reticle = ui.transform.Find("Gun Reticle Canvas").gameObject;
                reticle.SetActive(true);
            }
        }
        else
        {
            // If the inventory is inactive, activate it (open it)
            inventoryPanel.SetActive(true);
            fps.enabled = false;
            input.enabled = false;
            weapon.enabled = false;
            inventoryManager.ListItems(); // Call the method to list items
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
        }
    }
}
