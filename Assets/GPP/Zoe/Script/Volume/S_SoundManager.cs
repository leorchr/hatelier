using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum soundType
{
    Door_Open,
    Door_Close,
    Push_Pull,
    Bin,
    Step,
    Crafting_Object,
    Pulley,
    Ambiances,
    Crafting_Mat,
    Stuff_Object,
    Drop_object,
    Interaction_Plate,
    Grass_Step,
    Workshop_Step
}
public class S_SoundManager : MonoBehaviour
{
    
    public static S_SoundManager instance;

    private S_SoundBank sb;

    public AudioSource musicSource, effectSource;
    private void Awake()
    {
        sb = GetComponent<S_SoundBank>();
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(soundType st)
    {
        AudioClip ac = sb.getSound(st);
        if(ac != null)
        {
            PlaySFX(ac);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }


}
