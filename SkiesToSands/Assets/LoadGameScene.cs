using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class AsyncSceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;  // The loading screen UI
    public Slider progressBar;        // A slider for the progress bar (optional)

    // Call this function to load the scene asynchronously
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // Show the loading screen
        loadingScreen.SetActive(true);

        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;  // Prevent the scene from activating until fully loaded

        // Update the progress bar while the scene is loading
        while (!asyncOperation.isDone)
        {
            // Update the progress bar (0.9 means the scene is fully loaded but not activated)
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            // If the load is complete (reaches 0.9), activate the scene
            if (asyncOperation.progress >= 0.9f)
            {
                // Optional: Wait for a certain condition before activating the scene (like showing 100% loaded)
                yield return new WaitForSeconds(1);  // This is just to simulate loading delay
                
                // Activate the scene
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Hide the loading screen
        loadingScreen.SetActive(false);
    }
}
