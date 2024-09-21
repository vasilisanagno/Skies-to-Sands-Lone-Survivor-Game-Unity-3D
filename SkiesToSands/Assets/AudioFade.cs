using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the Audio Source component
    public float fadeDuration = 10.0f; // Duration of the fade-out in seconds
    public float fadeStartTime = 53.0f; // Time in seconds when the fade should start

    private bool isFading = false;

    void Start()
    {
        ResetAndPlayAudio();
        // Start a coroutine to wait until the specified time and then start fading out
        StartCoroutine(StartFadeAtTime(fadeStartTime));
    }

     public void ResetAndPlayAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the audio if it's already playing
        }
        audioSource.time = 0f; // Reset the audio clip's playback time to the start
        audioSource.Play(); // Play the audio from the beginning
    }

    IEnumerator StartFadeAtTime(float time)
    {
        // Wait for the specified time (in seconds)
        yield return new WaitForSeconds(time);

        // Start fading out
        StartCoroutine(FadeOut(audioSource, fadeDuration));
    }

    public IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        isFading = true;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;

            yield return null;
        }

        source.Stop();  // Optionally stop the audio source after fade-out
        source.volume = startVolume;  // Reset the volume to its original value for reuse
        isFading = false;
        audioSource.time = 0f; // Reset the audio clip's playback time to the start
    }

    void Update()
    {
        // Optional: Display the current playback time for debugging purposes
        if (!isFading)
        {
            Debug.Log("Current Time: " + audioSource.time);
        }
    }
}

