using UnityEngine;

public class CursorManagerOnCloseButton : MonoBehaviour
{
    // Method to hide the cursor and lock it
    public void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
