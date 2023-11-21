using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class S_Option : MonoBehaviour
{
    [Header("Scene")]
    [Space]
    public GameObject panelMenu;
    public GameObject panelOption;
    public void CloseOption()
    {
        panelMenu.SetActive(true);
        panelOption.SetActive(false);
    }
}
