using UnityEngine;

public class ShootEffectsHandler : MonoBehaviour
{
    public ParticleSystem muzzleFlash;  // Assign in Inspector
    public AudioSource gunAudioSource;    // Assign in Inspector

    // This method will be called by the Animation Event
    public void Shoot()
    {
        // Instantiate or enable the muzzle flash
        muzzleFlash.Play();

        // Play the gunshot sound
        gunAudioSource.Play();
    }
}