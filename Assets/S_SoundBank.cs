using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Sounds
{
    public soundType type;
    public AudioClip[] sounds;
}

public class S_SoundBank : MonoBehaviour
{
    public Sounds[] soundBank;

    public AudioClip getSound(soundType type)
    {
        foreach(var s in soundBank)
        {
            if (s.type == type)
            {
                int i = Random.Range(0, s.sounds.Length);
                return s.sounds[i];
            }
        }
        return null;
    }

}
