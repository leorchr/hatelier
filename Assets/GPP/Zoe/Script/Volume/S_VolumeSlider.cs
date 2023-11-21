using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public float volumeValue;
    void Start()
    {
        volumeSlider.value = volumeValue;
        S_SoundManager.instance.ChangeMasterVolume(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(val => S_SoundManager.instance.ChangeMasterVolume(val));
    }
}
