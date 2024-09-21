using UnityEngine;

public class PlayerIconRotation : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform in the world
    public RectTransform playerIcon;  // Reference to the RectTransform of the player icon (UI Image)

    void Update()
    {
        // Get the player's Y-axis rotation
        float playerRotationY = player.eulerAngles.y;

        // Apply the rotation to the player icon on the minimap
        // Since UI rotates in 2D space, invert the Y-axis rotation to match the minimap's orientation
        playerIcon.localEulerAngles = new Vector3(0, 0, -playerRotationY + 90f);
    }
}
