using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Interact_mold : S_Interactable
{
    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";
    [Header("Inventory")]
    public Image reducedInventory;
    public GameObject mainInventoryGroup;

    public S_Materials material;
    private bool isInInventory = false;

    private void Update()
    {
        Debug.Log(isInInventory);
    }

    public override string GetDescription()
    {
            return description;
    }

    public override string GetMatDescription()
    {
            return material.description;
    }

    public override void Interact()
    {
        if (isInInventory == false)
        {
            mainInventoryGroup.gameObject.SetActive(true);
            reducedInventory.gameObject.SetActive(false);
            isInInventory = true;
        }
        else
        {
            mainInventoryGroup.gameObject.SetActive(false);
            reducedInventory.gameObject.SetActive(true);
            isInInventory = false;
        }
       
    }
    
}
