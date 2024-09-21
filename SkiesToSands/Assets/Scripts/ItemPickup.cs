using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool isPlayerInRange = false; // To check if the player is near
    private bool isMessageShown = false; // To track if the message has been shown

    void Update() {
        if (isPlayerInRange && !isMessageShown && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
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

    void Pickup() {
        InventoryManager.Instance.Add(item);
        // Set isPlayerInRange to false before the item is destroyed
        isPlayerInRange = false;
        Destroy(gameObject);
        isMessageShown = true; // Set the flag so it doesn't show again
    }
}
