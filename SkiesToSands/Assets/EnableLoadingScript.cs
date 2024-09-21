using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLoadingScript : MonoBehaviour
{
    public GameObject loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Activated");
        loadingCanvas.SetActive(true);
    }
}
