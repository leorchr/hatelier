using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSound : MonoBehaviour
{
    public static S_PlayerSound instance;
    [SerializeField] private AudioSource playerSource;

    
    [SerializeField] private List<AudioClip> workshopFootsteps;
    [SerializeField] private List<AudioClip> gardenFootsteps;

    public enum groundType
    {
        Workshop,
        Garden
    }

    private groundType currentGround = groundType.Workshop;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playerSource = GetComponent<AudioSource>();
    }
    
    public void StepSound()
    {
        //if(Player in the workshop)
        RaycastHit hit;
        Vector3 rayPos = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z) ;
        if (Physics.Raycast(rayPos, Vector3.down, out hit, 0.5f))
        {
            Debug.Log(hit.collider.gameObject.layer);
            switch (hit.collider.gameObject.layer)
            {
                case 6:
                    currentGround = groundType.Garden;
                    break;
                case 7:
                    currentGround = groundType.Workshop;
                    break;
                default:
                    break;
            }
           
        }

        int currentSound;
        switch (currentGround)
        {
            case groundType.Workshop:
                currentSound = Random.Range(0, workshopFootsteps.Count);
                playerSource.clip = workshopFootsteps[currentSound];
                playerSource.Play();
                break;
            case groundType.Garden:
                currentSound = Random.Range(0, gardenFootsteps.Count);
                playerSource.clip = gardenFootsteps[currentSound];
                playerSource.Play();
                break;
            default : break;
        }
    }
}
