using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class S_VolumeSlider : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Update()
    {
        MusicSlider();
        SFXSlider();
    }

    public void MusicSlider()
    {
        audioMixer.SetFloat("music", musicSlider.value);
    }

    public void SFXSlider()
    {
        audioMixer.SetFloat("sfx", sfxSlider.value);

    }
}
