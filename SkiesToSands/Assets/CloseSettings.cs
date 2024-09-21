using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettings : MonoBehaviour
{
    public GameObject disableCanvas;
    public GameObject enableCanvas;

    // Start is called before the first frame update
    public void CloseSettingsCanvas ()
    {
        enableCanvas.SetActive(true);
        disableCanvas.SetActive(false);
    }
}
