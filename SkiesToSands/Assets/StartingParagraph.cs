using System.Collections;
using UnityEngine;
using TMPro;  // Add this if using TextMeshPro
using UnityEngine.SceneManagement;

public class IntroNarrative : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Drag your TextMeshPro text here
    public GameObject canvas;  // Drag the Canvas object here
    public float delayBetweenLetters = 0.05f; // Time in seconds between each letter
    public float delayBetweenParagraphs = 1.0f; // Time in seconds between each paragraph
    public float delayBeforeDisappear = 2.0f; // Time in seconds before the canvas disappears

    public GameObject[] objectsToEnable; // Array of game objects to enable when the narrative is over

    private string[] narrativeParagraphs = new string[]
    {
        "You are Captain Ryan, a seasoned Air Force pilot known for your precision and bravery. You were en route to a critical mission when disaster struck.",
        "High above enemy territory, your aircraft's engines suddenly failed, sending you plummeting into the unknown. With all systems down, you fought to maintain control, but the crash was inevitable.",
        "When you finally come to, the world is eerily silent. The smell of burning metal fills the air, and the only sounds are the distant cries of unfamiliar creatures and the gentle lapping of waves against the shore.",
        "As your vision clears, you realize that you've landed on a remote, uncharted island—far from any hope of rescue.",
        "With no communication and limited supplies, survival becomes your mission. But you're not alone on this island. Strange ruins, hidden dangers, and whispers of a lost civilization surround you.",
        "As you gather your wits, one thought drives you forward: escape... or die trying.",
        "Your journey begins now, Captain Ryan. The skies may have betrayed you, but your fight is far from over."
    };

    private Coroutine narrativeCoroutine;
    private bool isSkipping = false;

    void OnEnable()
    {
        if (canvas.activeSelf)
        {
            // Start the coroutine to type out the narrative when the canvas becomes active
            if (narrativeCoroutine != null)
            {
                StopCoroutine(narrativeCoroutine); // Stop any existing coroutine to prevent overlapping
            }
            narrativeCoroutine = StartCoroutine(TypeNarrative());
        }
    }

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isSkipping)
            {
                isSkipping = true; // Set skipping flag to true
                if (narrativeCoroutine != null)
                {
                    StopCoroutine(narrativeCoroutine); // Stop the current coroutine
                }
                SkipNarrative(); // Immediately skip the narrative and activate objects
            }
        }
    }

    private IEnumerator TypeNarrative()
    {
        foreach (string paragraph in narrativeParagraphs)
        {
            textComponent.text = ""; // Clear the text component for the new paragraph
            foreach (char letter in paragraph.ToCharArray())
            {
                textComponent.text += letter; // Add one letter at a time
                yield return new WaitForSeconds(delayBetweenLetters); // Wait before adding the next letter

                // Check if skipping has been triggered
                if (isSkipping)
                {
                    yield break; // Exit the coroutine if skipping
                }
            }
            
            // Wait between paragraphs
            yield return new WaitForSeconds(delayBetweenParagraphs);

            // Check if skipping has been triggered
            if (isSkipping)
            {
                yield break; // Exit the coroutine if skipping
            }
        }

        // Wait for the specified delay before disappearing
        yield return new WaitForSeconds(delayBeforeDisappear);

        SkipNarrative();
    }

    private void SkipNarrative()
    {
        SceneManager.LoadScene("GameScene");
        // Immediately enable the specified objects
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
}
