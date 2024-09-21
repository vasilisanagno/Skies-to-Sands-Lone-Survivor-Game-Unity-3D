using UnityEngine;

public class CanvasCursorControl : MonoBehaviour
{
    // Reference to the Canvas object
    public Canvas targetCanvas;
    public Texture2D customCursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Update()
    {
        // Check if the Canvas is active
        if (targetCanvas != null && targetCanvas.gameObject.activeInHierarchy)
        {
            // Show the cursor and unlock it
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
        }
        else
        {
            // Hide the cursor and lock it (optional, based on your needs)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
    }
}
