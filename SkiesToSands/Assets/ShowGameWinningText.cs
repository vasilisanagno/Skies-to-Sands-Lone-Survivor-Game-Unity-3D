using System.Collections;
using UnityEngine;
using TMPro;  // Add this if using TextMeshPro
using UnityEngine.SceneManagement;

public class WinNarrative : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Drag your TextMeshPro text here
    public GameObject currentCanvas;  // The current canvas to disable after the text finishes
    public GameObject nextCanvas;  // The next canvas to activate after the text finishes
    public GameObject[] objectsToDisable;  // Array of objects to disable when the script starts
    public MonoBehaviour[] componentsToDisable; // Array of components to disable when the script starts

    public float delayBetweenLetters = 0.05f; // Time in seconds between each letter
    public float delayBetweenParagraphs = 1.0f; // Time in seconds between each paragraph
    public float delayBeforeDisappear = 2.0f; // Time in seconds before the current canvas disappears

    private string[] narrativeParagraphs = new string[]
    {
        "After securing the radio, you manage to tune into the right frequency and send out a desperate call for help." +
        " The response crackles through the static: you're" +
        " instructed to make your way to the shore, where a rescue plane will be waiting to extract you.",
        "With renewed determination, you navigate the perilous journey to the shoreline," +
        " avoiding dangers and overcoming obstacles. As the plane descends, you feel a surge of" +
        " relief—rescue is within reach. You board the plane, watching the island fade into the" +
        " distance, and finally, you return home, safe and sound, leaving the harrowing experience behind.",
        "As you settle into your seat on the plane, the hum of the engines becomes a comforting rhythm." +
        " The island, once a place of peril and uncertainty, shrinks below, disappearing beneath the clouds." +
        " You take a deep breath, the weight of your ordeal lifting as the reality of safety sinks in.",
        " The world you knew is just a flight away, and the promise of home, of familiar faces and comfort" +
        " fills you with peace. This chapter of your life, a test of survival and resilience, is closing." +
        " Ahead lies a new beginning—one where the echoes of this adventure will remain," +
        " but only as memories of a journey that proved your strength.",
        "Thank you for playing!"
    };

    private bool narrativeStarted = false;  // To track if the narrative has already started
    private Coroutine narrativeCoroutine;   // To hold the running coroutine
    private bool isSkipping = false;        // To handle skipping

    void Update()
    {
        // Check if the narrative hasn't started yet
        if (!narrativeStarted)
        {
            narrativeStarted = true;  // Set the flag to true to prevent restarting
            StartNarrative();
        }

        // Check if the "Enter" key is pressed to skip the narrative
        if (Input.GetKeyDown(KeyCode.Return) && !isSkipping)
        {
            isSkipping = true;  // Set the skipping flag to true
            if (narrativeCoroutine != null)
            {
                StopCoroutine(narrativeCoroutine);  // Stop the current coroutine
            }
            SkipNarrative();  // Proceed with skipping the narrative
        }
    }

    void StartNarrative()
    {
        // Disable the specified objects
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        // Disable the specified components
        foreach (MonoBehaviour comp in componentsToDisable)
        {
            comp.enabled = false;
        }

        // Start the coroutine to type out the narrative
        narrativeCoroutine = StartCoroutine(StartText());
    }

    private IEnumerator StartText()
    {
        for (int i = 0; i < narrativeParagraphs.Length; i++)
        {
            textComponent.text = ""; // Clear the text component for the new paragraph

            // Check if we're at the last string to center it and apply size tag separately
            if (i == narrativeParagraphs.Length - 1)
            {
                textComponent.alignment = TextAlignmentOptions.Center; // Center align the text
                textComponent.text = "<size=300%>"; // Set size before typing text
            }

            foreach (char letter in narrativeParagraphs[i].ToCharArray())
            {
                textComponent.text += letter;  // Append the letter to the text component
                yield return new WaitForSeconds(delayBetweenLetters);  // Wait before adding the next letter
            }

            // After finishing the paragraph, close the size tag if it was opened
            if (i == narrativeParagraphs.Length - 1)
            {
                textComponent.text += "</size>";  // Close the size tag
            }

            // Wait between paragraphs
            yield return new WaitForSeconds(delayBetweenParagraphs);
        }

        // Wait for the specified delay before switching canvases
        yield return new WaitForSeconds(delayBeforeDisappear);

        // Call the method to proceed with the end of the narrative
        EndNarrative();
    }

    private void SkipNarrative()
    {
        // Immediately stop typing the narrative and move to the next steps
        EndNarrative();
    }

    private void EndNarrative()
    {
        // Reload the scene or perform actions after the narrative ends
        SceneManager.LoadScene("StartScene");
    }
}
