using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class S_HideBeforePlay : MonoBehaviour
{
    public static S_HideBeforePlay instance;

    [Header("Left Stick")]
    [SerializeField] private GameObject leftStick;
    [SerializeField] private GameObject handleLS;
    [Header("Interact Button")]
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject imageIB;
    [Header("Pause Button")]
    [SerializeField] private GameObject pauseButtton;
    [Header("Inventory (Barre)")]
    //[SerializeField] private GameObject inventoryComposant0;
    //[SerializeField] private GameObject inventoryComposant1;
    //[SerializeField] private GameObject inventoryComposant2;
    //[SerializeField] private GameObject inventoryComposant3;
    //[SerializeField] private GameObject inventoryComposant4;
    //[SerializeField] private GameObject inventoryComposant5;
    //[SerializeField] private GameObject inventoryComposant6;
    [Header("Inventory + drop Button")]
    //[SerializeField] private GameObject inventoryComposant7;
    //[SerializeField] private GameObject inventoryComposant8;
    [SerializeField] private GameObject dropButton;

    public Color colorAlphaLow;
    public Color colorAlphaHigh;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!S_WarmUpTimer.instance.gameBegin)
        {
            leftStick.GetComponent<S_LeftStick>().enabled = false;
            interactButton.GetComponent<Button>().enabled = false;
            pauseButtton.GetComponent<Button>().enabled = false;
            dropButton.GetComponent<Button>().enabled = false;
            //Image Alpha
            leftStick.GetComponent<Image>().color = colorAlphaLow;
            handleLS.GetComponent<Image>().color = colorAlphaLow;
            interactButton.GetComponent<Image>().color = colorAlphaLow;
            imageIB.GetComponent<Image>().color = colorAlphaLow;
            pauseButtton.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant0.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant1.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant2.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant3.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant4.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant5.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant6.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant7.GetComponent<Image>().color = colorAlphaLow;
            //inventoryComposant8.GetComponent<Image>().color = colorAlphaLow;
            //dropButton.GetComponent<Image>().color = colorAlphaLow;
        }
        else
        {
            leftStick.GetComponent<S_LeftStick>().enabled = true;
            interactButton.GetComponent<Button>().enabled = true;
            pauseButtton.GetComponent<Button>().enabled= true;
            dropButton.GetComponent<Button>().enabled= true;
            //Image Alpha
            leftStick.GetComponent<Image>().color = colorAlphaHigh;
            handleLS.GetComponent<Image>().color = colorAlphaHigh;
            interactButton.GetComponent<Image>().color = colorAlphaHigh;
            imageIB.GetComponent<Image>().color = colorAlphaHigh;
            pauseButtton.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant0.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant1.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant2.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant3.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant4.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant5.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant6.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant7.GetComponent<Image>().color = colorAlphaHigh;
            //inventoryComposant8.GetComponent<Image>().color = colorAlphaHigh;
            //dropButton.GetComponent<Image>().color = colorAlphaHigh;
        }
    }
}
