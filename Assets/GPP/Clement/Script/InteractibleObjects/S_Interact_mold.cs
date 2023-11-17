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
    public Image mainInventory;

    public S_Materials material;
    private bool isInInventory = false;
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
            mainInventory.gameObject.SetActive(true);
            reducedInventory.gameObject.SetActive(false);
            isInInventory = true;
        }
        else
        {
            mainInventory.gameObject.SetActive(false);
            reducedInventory.gameObject.SetActive(true);
            isInInventory = false;
        }
       
    }
    
}
