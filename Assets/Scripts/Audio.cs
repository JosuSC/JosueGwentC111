using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosionSound()
    {
       
        audioSource.clip = Resources.Load<AudioClip>("explosion");

       
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
