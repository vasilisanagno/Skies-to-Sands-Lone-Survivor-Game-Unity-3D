using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    private GameObject ui;
    private GameObject reticle;
    private GameObject carbine;
    private GameObject player;
    private GameObject mainCamera;
    private GameObject fpc;
    public void ShowMessage(Image notificationText)
    {
        // Set the text
        ui = GameObject.Find("UI");
        reticle = ui.transform.Find("Gun Reticle Canvas").gameObject;
        reticle.SetActive(false);
        notificationText.gameObject.SetActive(true);
        // Start coroutine to hide the message after 3 seconds
        StartCoroutine(HideMessageAfterDelay(3f, notificationText));
    }

    private IEnumerator HideMessageAfterDelay(float delay, Image notificationText)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        notificationText.gameObject.SetActive(false); // Hide the text
        player = GameObject.Find("PlayerCapsuleOG");
        mainCamera = player.transform.Find("MainCamera").gameObject;
        fpc = mainCamera.transform.Find("First Person Camera").gameObject;
        carbine = fpc.transform.Find("Carbine").gameObject;
        if (carbine.activeInHierarchy) {
            ui = GameObject.Find("UI");
            reticle = ui.transform.Find("Gun Reticle Canvas").gameObject;
            reticle.SetActive(true);
        }
    }
}