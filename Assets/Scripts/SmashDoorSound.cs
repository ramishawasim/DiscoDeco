using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDoorSound : MonoBehaviour
{
    // Start is called before the first frame update

    public float soundTimeOutInSecondsFrom, soundTimeOutInSecondsTo;


    private AudioSource sound;
    private float soundTimeOutInSeconds;
    private float soundLength;
    private float timeSinceLastSound;
    private bool busy;


    void Start()
    {
        sound = GetComponent<AudioSource>();
        soundLength = sound.clip.length;
    }

    public void PlaySound()
    {
        if (CanPlay()) StartCoroutine("PlaySound_");
    }

    private bool CanPlay()
    {
        return !busy && Time.time - timeSinceLastSound > soundTimeOutInSeconds;
    }

    private IEnumerator PlaySound_()
    {
        soundTimeOutInSeconds = Random.Range(soundTimeOutInSecondsFrom, soundTimeOutInSecondsTo);
        timeSinceLastSound = Time.time;
        busy = true;
        sound.Play();
        yield return new WaitForSeconds(soundLength);
        busy = false;
        yield return null;
    }
}
