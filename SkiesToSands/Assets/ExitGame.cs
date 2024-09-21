using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public Texture2D customCursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    public Button playAgainButton;
    public FirstPersonController fps;
    public PlayerInput input;
    public Weapon weapon;

    void Start()
    {
       gameOverCanvas.SetActive(false);    
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // When the application is focused, set the custom cursor
            Cursor.SetCursor(customCursorTexture, hotSpot, cursorMode);
        }
        else
        {
            // When the application loses focus, revert to the default cursor
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    public void onPlayerDeath(GameObject canvas) {
        // Deselect any selected UI element
        EventSystem.current.SetSelectedGameObject(null);
        // Activate the GameOverCanvas
        canvas.SetActive(true);
        fps.enabled = false;
        input.enabled = false;
        weapon.enabled = false;

        // Show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void HideGameOverCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
        fps.enabled = true;
        input.enabled = true;
        weapon.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}