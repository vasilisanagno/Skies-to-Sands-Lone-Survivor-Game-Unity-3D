using UnityEngine;
// using UnityEngine.SceneManagement;

public class ToggleObjects : MonoBehaviour
{
    public GameObject objectToDisable; // Assign the GameObject to disable
    public GameObject objectToEnable;  // Assign the GameObject to enable
    public GameObject SettingsMenuToDisable;  // Assign the GameObject to enable

    // This method will be called when the button is pressed
    public void Toggle()
    {
        // Hide the cursor and lock it (optional, based on your needs)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // Disable the first object
        }

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true); // Enable the second object
        }

        if (SettingsMenuToDisable != null)
        {
            SettingsMenuToDisable.SetActive(false); // Disable the first object
        }
        
        // SceneManager.LoadScene("GameScene");
    }
}
