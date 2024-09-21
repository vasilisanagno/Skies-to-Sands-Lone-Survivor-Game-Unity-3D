using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use this namespace for TextMeshPro

public class ShowTextsEnteringForest : MonoBehaviour
{
    public TextMeshProUGUI displayText; // The TextMeshPro component on your Canvas
    public string[] messages; // An array of strings to display
    public float displayDuration = 5f; // Duration to display each message
    public GameObject uiObject;

    void Start()
    {
        uiObject.SetActive(false);    
    }

    void OnTriggerEnter(Collider player) {
        if(player.gameObject.tag == "Player") {
            uiObject.SetActive(true);
            StartCoroutine("DisplayMessages");
        }
    }

    IEnumerator DisplayMessages()
    {
        foreach (string message in messages)
        {
            displayText.text = message; // Set the current text
            displayText.enabled = true; // Make sure it's visible

            yield return new WaitForSeconds(displayDuration); // Wait for the specified time

            displayText.enabled = false; // Hide the text after the duration
        }
        Destroy(uiObject);
        // Optional: If you want to do something after all messages are displayed, add it here
    }
    
}
