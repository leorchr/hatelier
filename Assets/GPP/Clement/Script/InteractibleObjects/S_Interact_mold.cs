using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Interact_mold : S_Interactable
{
    public static S_Interact_mold instance;

    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";
    [Header("Inventory")]
    public Image reducedInventory;
    public Image mainInventory;

    public S_Materials material;
    private bool isInInventory = false;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Update()
    {
        if(isInInventory == true && (Input.GetMouseButtonDown(1)))
        {
            OnInventoryVisible();
        }
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
        isInInventory = true;
        S_Player_Interaction.instance.OnInteraction();
        mainInventory.gameObject.SetActive(true);
        reducedInventory.gameObject.SetActive(false);
        //display inventory ui
    }

    public void OnInventoryVisible()
    {
        if (isInInventory == true )
        {
            mainInventory.gameObject.SetActive(false);
            reducedInventory.gameObject.SetActive(true);
            isInInventory = false;
        }
    }
}
