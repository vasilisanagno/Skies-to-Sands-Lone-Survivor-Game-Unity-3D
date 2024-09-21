using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using StarterAssets; 

public class LoadingScreenBarSystem : MonoBehaviour {
    public GameObject bar;
    public Text loadingText;
    public bool backGroundImageAndLoop;
    public float LoopTime;
    public GameObject[] backgroundImages;
    [Range(0,1f)] public float vignetteEfectVolue; // Must be a value between 0 and 1
    AsyncOperation async;
    Image vignetteEfect;

    public GameObject canvas;  // Drag the Canvas object here

    private FirstPersonController cameraController;
    public GameObject[] objectsToEnable; // Array of game objects to enable when the narrative is over
    public GameObject PlayerCapsule;
    private PlayerHealth playerHealth;

    public void loadingScreen (int sceneNo)
    {
        // Disable all objects except the loading canvas
        this.gameObject.SetActive(true);
        StartCoroutine(Loading(sceneNo));
    }

    public void Update()
    {
        // Time needed to fill the bar (in seconds)
        float totalTimeToComplete = 15f; 
        
        // Calculate the amount the bar should fill per second
        float fillRate = 1f / totalTimeToComplete;  // This ensures the bar fills 100% in 15 seconds
        
        // Increment the bar's scale based on the time that has passed since the last frame
        bar.transform.localScale += new Vector3(fillRate * Time.deltaTime, 0, 0);
        
        // Ensure the bar doesn't exceed 100% (x scale of 1)
        if (bar.transform.localScale.x >= 1)
        {
            bar.transform.localScale = new Vector3(1, bar.transform.localScale.y, bar.transform.localScale.z);

            // Perform action when the loading reaches 100%
            OnLoadingComplete();
        }

        // Update the loading text if it's not null
        if (loadingText != null)
        {
            loadingText.text = (100 * bar.transform.localScale.x).ToString("####") + "%";
        }
    }

    // This method will be called when the loading bar reaches 100%
    private void OnLoadingComplete()
    {
        canvas.SetActive(false);
        cameraController.enabled = true;
        playerHealth = PlayerCapsule.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;
        
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }

    private void Start()
    {
        // Find the CameraController script dynamically at the start
        cameraController = FindObjectOfType<FirstPersonController>();
        cameraController.enabled = false;
        vignetteEfect = transform.Find("VignetteEfect").GetComponent<Image>();
        vignetteEfect.color = new Color(vignetteEfect.color.r,vignetteEfect.color.g,vignetteEfect.color.b,vignetteEfectVolue);

        if (backGroundImageAndLoop)
            StartCoroutine(transitionImage());
    }


    // The pictures change according to the time of
    IEnumerator transitionImage ()
    {
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            yield return new WaitForSeconds(LoopTime);
            for (int j = 0; j < backgroundImages.Length; j++)
                backgroundImages[j].SetActive(false);
            backgroundImages[i].SetActive(true);           
        }
    }

    // Activate the scene 
    IEnumerator Loading (int sceneNo)
    {
        async = SceneManager.LoadSceneAsync(sceneNo);
        async.allowSceneActivation = false;

        // Continue until the installation is completed
        while (async.isDone == false)
        {
            bar.transform.localScale = new Vector3(async.progress,0.9f,1);

            if (loadingText != null)
                loadingText.text = (100 * bar.transform.localScale.x).ToString("####") + "%";

            if (async.progress == 0.9f)
            {
                bar.transform.localScale = new Vector3(1, 0.9f, 1);
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
