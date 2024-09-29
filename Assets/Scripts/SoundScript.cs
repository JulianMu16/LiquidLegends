using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource

    void Start()
    {
        audioSource.Play();  // Start playing the audio
    }

    void StopAudio()
    {
        audioSource.Stop();  // Stop the audio
    }
}