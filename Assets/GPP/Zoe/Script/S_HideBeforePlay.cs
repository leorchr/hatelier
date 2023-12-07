using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class S_HideBeforePlay : MonoBehaviour
{
    public static S_HideBeforePlay instance;

    [SerializeField] private List<GameObject> uiElements;

    [SerializeField] private float colorAlphaLow;
    [SerializeField] private float colorAlphaHigh;

    [Header("GameObject with functions")]
    [SerializeField] private GameObject leftStick;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject dropButton;

    [TextArea][Tooltip("Ok")] public string noteList;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!S_WarmUpTimer.instance.gameBegin)
        {
            foreach (var elements in uiElements)
            {
                Color elementsColor = elements.GetComponent<Image>().color;
                elementsColor = new Color(elementsColor.r, elementsColor.g, elementsColor.b, colorAlphaLow);
                elements.GetComponent<Image>().color = elementsColor;
            }

            leftStick.GetComponent<S_LeftStick>().enabled = false;
            interactButton.GetComponent<Button>().enabled = false;
            pauseButton.GetComponent<Button>().enabled = false;
            dropButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            foreach (var elements in uiElements)
            {
                Color elementsColor = elements.GetComponent<Image>().color;
                elementsColor = new Color(elementsColor.r, elementsColor.g, elementsColor.b, colorAlphaHigh);
                elements.GetComponent<Image>().color = elementsColor;
            }

            leftStick.GetComponent<S_LeftStick>().enabled = true;
            interactButton.GetComponent<Button>().enabled = true;
            pauseButton.GetComponent<Button>().enabled= true;
            dropButton.GetComponent<Button>().enabled= true;
        }
    }
}
