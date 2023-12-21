using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class S_HideBeforePlay : MonoBehaviour
{
    public static S_HideBeforePlay instance;

    [Header("GameObject")]
    public GameObject leftStick;
    public GameObject interactButton;
    public GameObject pauseButton;
    public GameObject dropButton;



    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!S_WarmUpTimer.instance.gameBegin)
        {
            
            
            //Functions
            leftStick.GetComponent<S_LeftStick>().enabled = false;
            interactButton.GetComponent<Button>().enabled = false;
            pauseButton.GetComponent<Button>().enabled = false;
            dropButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            

            //Functions
            leftStick.GetComponent<S_LeftStick>().enabled = true;
            interactButton.GetComponent<Button>().enabled = true;
            pauseButton.GetComponent<Button>().enabled= true;
            dropButton.GetComponent<Button>().enabled= true;
        }
    }
}
