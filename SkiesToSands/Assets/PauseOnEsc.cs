using UnityEngine;
using StarterAssets; 

public class ToggleCanvasOnEsc : MonoBehaviour
{
    public GameObject canvasToToggle;  // Assign your canvas in the Inspector
    public GameObject[] objectsToDisable;  // Assign other GameObjects to disable (e.g., player controls, other UI elements)
    
    public bool isPaused = false;
    public bool isActive = false;

    private FirstPersonController cameraController;

    private void Start()
    {
        // Find the CameraController script dynamically at the start
        cameraController = FindObjectOfType<FirstPersonController>();

        if (cameraController == null)
        {
            Debug.LogError("CameraController script not found!");
        }
    }

    private void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed");

            // Toggle the canvas active state
            canvasToToggle.SetActive(!isActive);
            Cursor.visible = !isActive;
            
            // Toggle the isPaused state
            isPaused = !isPaused;
            isActive = !isActive;
            if (isActive) {
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Enable or disable the camera movement if CameraController was found
            if (cameraController != null)
            {
                cameraController.enabled = !isPaused;
            }

            // Enable or disable the other objects
            if (isPaused)
            {    
                Time.timeScale = 0f;  // Freeze the game
            }
            else
            {
                Time.timeScale = 1f;  // Resume the game
            }
        }
    }
}
