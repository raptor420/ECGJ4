using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgentSoundManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] clips;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Playaudio(int index)
    {


        source.Stop();
            source.clip = clips[index];

            source.PlayOneShot(clips[index]);
        
    }
}
