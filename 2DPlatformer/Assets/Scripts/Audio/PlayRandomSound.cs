using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{

    //Uses the choices from the Audio Source


    public AudioSource audioSource;
    public AudioClip[] audioSources;

    public bool continuous = false;


    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (continuous && !audioSource.isPlaying)
        {
            PlaySound();
        }
    }

    //Call play sound to play a random sound from the range of the attached stuff
    public void PlaySound(float initialOffset = 0)
    {
        audioSource.clip = audioSources[Random.Range(0, audioSources.Length)];
        audioSource.time = initialOffset;
        audioSource.Play();
    }
}
