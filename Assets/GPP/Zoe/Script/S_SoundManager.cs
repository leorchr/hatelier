using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SoundManager : MonoBehaviour
{
    public static S_SoundManager instance;

    public AudioSource audioSource;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }


}
