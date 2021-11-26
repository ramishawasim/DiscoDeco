using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource mooSound;
    public AudioSource grumbleSound;

    public float soundTimeOutInSecondsFrom, soundTimeOutInSecondsTo;

    private float soundTimeOutInSeconds;

    private float mooSoundLength, grumbleSoundLength;

    private bool busy;
    private float timeSinceLastSound;


    public void Start()
    {
        busy = false;

        mooSoundLength = mooSound.clip.length;
        grumbleSoundLength = grumbleSound.clip.length;
    }


    public void PlayMooSound()
    {
        if (CanPlay()) StartCoroutine("PlayMooSound_");
    }

    public void PlayGrumbleSound()
    {
        if (CanPlay()) StartCoroutine("PlayGrumbleSound_");
    }

    private bool CanPlay()
    {
        return !busy && Time.time - timeSinceLastSound > soundTimeOutInSeconds;
    }

    private IEnumerator PlayMooSound_()
    {
        soundTimeOutInSeconds = Random.Range(soundTimeOutInSecondsFrom, soundTimeOutInSecondsTo);
        timeSinceLastSound = Time.time;
        busy = true;
        mooSound.Play();
        yield return new WaitForSeconds(mooSoundLength);
        busy = false;
        yield return null;
    }

    private IEnumerator PlayGrumbleSound_()
    {
        soundTimeOutInSeconds = Random.Range(soundTimeOutInSecondsFrom, soundTimeOutInSecondsTo);
        timeSinceLastSound = Time.time;
        busy = true;
        grumbleSound.Play();
        yield return new WaitForSeconds(grumbleSoundLength);
        busy = false;
        yield return null;
    }
}
