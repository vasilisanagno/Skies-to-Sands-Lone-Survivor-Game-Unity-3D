using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    // This method will be called when the button is pressed
    public void ExitGame()
    {
        // Quit the application
        Application.Quit();

        // Note: This line will only work in a built application. It won't exit the editor.
        Debug.Log("Game is exiting...");

        // In the Unity editor, this line will simulate quitting the game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
