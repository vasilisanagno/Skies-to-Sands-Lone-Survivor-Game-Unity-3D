using UnityEngine;

public class TriggerTextDisplay : MonoBehaviour
{
    public GameObject textMeshObject;  // Reference to the GameObject that contains the TextMesh

    private void Start()
    {
        // Initially, make sure the TextMesh object is inactive (hidden)
        if (textMeshObject != null)
        {
            textMeshObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is the player
        if (other.CompareTag("Player"))
        {
            // Activate the TextMesh object
            if (textMeshObject != null)
            {
                textMeshObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the collider is the player
        if (other.CompareTag("Player"))
        {
            // Deactivate the TextMesh object
            if (textMeshObject != null)
            {
                textMeshObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        // Ensure the text is hidden when the item is destroyed
        if (textMeshObject != null)
        {
            textMeshObject.SetActive(false);
        }
    }
}
