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

    public AudioSource musicSource, effectSource, gardenSource, workshopSource;

    public float timeToAdjustVolumeAmbiance = 1;


    public bool isInGarden = false;

    private float gv = 0, wv = 0;
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
        gardenSource.volume = isInGarden ? 1 : 0;
        workshopSource.volume = isInGarden ? 0 : 1;
    }

    public void PlaySound(soundType st)
    {
        AudioClip ac = sb.getSound(st);
        if(ac != null)
        {
            PlaySFX(ac);
        }
    }

    private void Update()
    {
        if (isInGarden && (workshopSource.volume != 0 || gardenSource.volume != 1))
        {
            Debug.Log("Garden");
            workshopSource.volume = Mathf.SmoothDamp(workshopSource.volume, 0, ref wv, timeToAdjustVolumeAmbiance);
            gardenSource.volume = Mathf.SmoothDamp(gardenSource.volume, 1, ref gv, timeToAdjustVolumeAmbiance);
        }
        else if (!isInGarden && (workshopSource.volume != 1 || gardenSource.volume != 0))
        {
            Debug.Log("not Garden");

            workshopSource.volume = Mathf.SmoothDamp(workshopSource.volume, 1, ref wv, timeToAdjustVolumeAmbiance);
            gardenSource.volume = Mathf.SmoothDamp(gardenSource.volume, 0, ref gv, timeToAdjustVolumeAmbiance);
        }
    }

    public void toggleIsInGarden()
    {
        isInGarden = !isInGarden;
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
