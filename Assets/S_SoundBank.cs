using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class S_Sounds
{
    
}

[Serializable]
public class Sounds
{
    public soundType type;
    public AudioClip[] sounds;
}

public class S_SoundBank : MonoBehaviour
{
    public Sounds[] soundBank;

}
