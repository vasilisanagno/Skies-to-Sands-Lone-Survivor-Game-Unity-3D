using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsAudio : MonoBehaviour
{
    public AudioSource cricketsAudioSource;
    public float fadeTime = 5;
    public float minTimeOff = 5;
    public float maxTimeOff = 15;
    float timeOff;
    public float minTimeOn = 5;
    public float maxTimeOn = 15;
    float timeOn;

    public float minVol = 0.5f;
    public float maxVol = 1;
    float cricketsVol;

    public float timer;
    public float timeTillNextEvent;

    bool cricketsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseValues();
        timeTillNextEvent = timeOff;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeTillNextEvent) {
            if (cricketsPlaying) {
                StartCoroutine(FadeCrickets(cricketsAudioSource.volume,0,fadeTime));
                cricketsPlaying = false;
                RandomiseValues();
                timeTillNextEvent = timeOff + fadeTime;
            } else if (!cricketsPlaying) {
                StartCoroutine(FadeCrickets(0,cricketsVol,fadeTime));
                cricketsPlaying = true;
                RandomiseValues();
                timeTillNextEvent = timeOn + fadeTime;
            }

            timer = 0;
        }
    }

    void RandomiseValues() {
        timeOff = Random.Range(minTimeOff, maxTimeOff);
        timeOn = Random.Range(minTimeOn, maxTimeOn);
        cricketsVol = Random.Range(minVol, maxVol);
    }

    IEnumerator FadeCrickets(float startValue, float endValue, float duration) {
        float currentTime = 0;

        while (currentTime <= duration) {
            cricketsAudioSource.volume = Mathf.Lerp (startValue, endValue, (currentTime/duration));
            currentTime += Time.deltaTime;

            yield return null;
        }
    }
}
