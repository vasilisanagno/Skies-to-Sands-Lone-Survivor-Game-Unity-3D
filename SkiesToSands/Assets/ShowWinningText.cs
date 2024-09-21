using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public GameObject messageObject; // Reference to the GameObject you want to show
    public GameObject[] objectsToDisable; // Array of GameObjects to disable
    
    public float interactionDistance = 20f; // Set the maximum distance for interaction

    private bool isMessageShown = false; // To track if the message has been shown
    private bool isPlayerInRange = false; // To check if the player is near

    void Start()
    {
        messageObject.SetActive(false); // Make sure the message GameObject is hidden at the start
    }

    void Update()
    {
        // Check if the player presses the "E" key and the message has not been shown yet
        if (isPlayerInRange && !isMessageShown && Input.GetKeyDown(KeyCode.E))
        {
            DisableOtherObjects();
            ShowMessageObject();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming player has the "Player" tag
        {
            isPlayerInRange = true; // Player is near the item
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = false; // Player is no longer near
        }
    }

    void ShowMessageObject()
    {
        messageObject.SetActive(true); // Show the GameObject with the text
        isMessageShown = true; // Set the flag so it doesn't show again
    }

    void DisableOtherObjects()
    {
        // Disable all objects in the objectsToDisable array
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}