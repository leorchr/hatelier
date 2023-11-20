using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_OpenOption : MonoBehaviour
{
    [Header("Scene")]
    [Space]
    public GameObject panelMenu;
    public GameObject panelOption;

    private void Start()
    {
        panelMenu.SetActive(true);
        panelOption.SetActive(false);
    }
    public void OpenOption()
    {
        panelOption.SetActive(true);
        panelMenu.SetActive(false);
    }
}
