using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSound : MonoBehaviour
{
    public static S_PlayerSound instance;

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
    }
    
    public void StepSound()
    {
        //if(Player in the workshop)
        RaycastHit hit;
        Vector3 rayPos = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z) ;
        if (Physics.Raycast(rayPos, Vector3.down, out hit, 0.5f))
        {
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

        

        switch (currentGround)
        {
            case groundType.Workshop:
                S_SoundManager.instance.PlaySound(soundType.Workshop_Step);
                break;
            case groundType.Garden:
                S_SoundManager.instance.PlaySound(soundType.Grass_Step);
                break;
            default : break;
        }
    }

    
}
