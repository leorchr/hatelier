using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Inventory : MonoBehaviour
{

    public static S_UI_Inventory instance;   
    [SerializeField] private List<GameObject> images = new List<GameObject>();

    private void Awake()

    {
        if (!instance) instance = this;
    }

    public void DisplayIcon()
    {
        List<S_Materials> materials = S_Inventory.instance.GetMaterials();
        for (int i = 0; i < materials.Count; i++)
        {
            images[i].GetComponent<Image>().sprite = materials[i].icone;
            images[i].SetActive(true);
        }
    }
}
