using UnityEngine;
// using UnityEngine.SceneManagement;

public class OpenSettings : MonoBehaviour
{
    public GameObject objectToDisable; // Assign the GameObject to disable
    public GameObject objectToEnable;  // Assign the GameObject to enable

    // This method will be called when the button is pressed
    public void Toggle()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true); // Enable the second object
        }

        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // Disable the first object
        }
    }
}
