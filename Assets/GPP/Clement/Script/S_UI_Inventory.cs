using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Inventory : MonoBehaviour
{

    public static S_UI_Inventory instance;
    [SerializeField] private GameObject image;

    private void Awake()

    {
        if (!instance) instance = this;
    }

    public void DisplayIcon()
    {
        S_Materials inventory = S_Inventory.instance.GetMaterials();
        image.GetComponent<Image>().sprite = inventory.icone;
        image.SetActive(true);
    }

    public void ClearIcon()
    {
        image.SetActive(false);
    }
}