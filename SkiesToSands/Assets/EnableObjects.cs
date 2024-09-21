using UnityEngine;
using StarterAssets;

public class EnableObjects : MonoBehaviour
{
    // Reference to the ToggleCanvasOnEsc script
    public ToggleCanvasOnEsc toggleCanvasScript;  // Assign this in the Inspector or find it dynamically in Start()
    private FirstPersonController cameraController;

    void Start()
    {
        // Find the CameraController script dynamically at the start
        cameraController = FindObjectOfType<FirstPersonController>();

        // Optional: Dynamically find the ToggleCanvasOnEsc script if not assigned in the Inspector
        if (toggleCanvasScript == null)
        {
            toggleCanvasScript = FindObjectOfType<ToggleCanvasOnEsc>();
        }
    }

    // Call this method to enable a specific object
    public void EnableAllObjects(GameObject objectToEnable)
    {
        objectToEnable.SetActive(true);  // Enable the GameObject
    }

    // Method to disable the canvas and hide the cursor
    public void DisableCanvas(GameObject canvas)
    {
        Cursor.visible = false;
        // Toggle the canvas active state
        if (toggleCanvasScript != null)
        {
            toggleCanvasScript.isActive = false;
            canvas.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            cameraController.enabled = !toggleCanvasScript.isPaused;
            Time.timeScale = 1f;  // Unfreeze the game
        }
    }

    // Resume game method
    public void ResumeGame()
    {
        if (toggleCanvasScript != null)
        {
            // Set isPaused to false and resume the game
            toggleCanvasScript.isPaused = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            cameraController.enabled = !toggleCanvasScript.isPaused;
            Time.timeScale = 1f;  // Unfreeze the game
        }
    }
}
