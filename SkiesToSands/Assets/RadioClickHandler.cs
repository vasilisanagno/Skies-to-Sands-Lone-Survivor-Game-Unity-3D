using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

   public class RadioClickHandler : MonoBehaviour
{
    public Image radioImage;  // Reference to the UI Image that will be shown

    void Start()
    {
        // Ensure the image is hidden at the start
        if (radioImage != null)
        {
            radioImage.gameObject.SetActive(false);
        }
    }

    // This function will be called when the radio is clicked
    void OnMouseDown()
    {
        if (radioImage != null)
        {
            radioImage.gameObject.SetActive(true);  // Show the image
        }
    }
}

