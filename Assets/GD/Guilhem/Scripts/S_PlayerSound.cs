using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSound : MonoBehaviour
{
    public static S_PlayerSound instance;
    [SerializeField] private AudioSource playerSource;

    [SerializeField] private List<AudioClip> workshopFootsteps;
    [SerializeField] private List<AudioClip> gardenFootsteps;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playerSource = GetComponent<AudioSource>();
    }
    
    public void StepSound()
    {
        //if(Player in the workshop)

        int currentSound = Random.Range(0, gardenFootsteps.Count);
        playerSource.clip = gardenFootsteps[currentSound];

        playerSource.Play();

        //if(Player in the garden)

        currentSound = Random.Range(0, gardenFootsteps.Count);
        playerSource.clip = gardenFootsteps[currentSound];

        playerSource.Play();
    }
}
