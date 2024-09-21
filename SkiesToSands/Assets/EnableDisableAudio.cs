using UnityEngine;

public class MuteAudioListener : MonoBehaviour
{
    public void MuteAudio()
    {
        AudioListener.volume = 0;  // Mute all sounds
    }

    public void UnmuteAudio()
    {
        AudioListener.volume = 1;  // Unmute all sounds
    }
}
