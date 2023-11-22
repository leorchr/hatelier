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

    public S_Materials mold;
    private bool isInInventory = false;

    private void Update()
    {
    }

    public override string GetDescription()
    {
            return description;
    }

    public override string GetMatDescription()
    {
            return mold.description;
    }

    public override void Interact()
    {
        if (isInInventory == false)
        {
            S_Menu_Manager.Instance.stopPlayer(false);
            mainInventoryGroup.gameObject.SetActive(true);
            reducedInventory.gameObject.SetActive(false);
            if (!S_Mold_Inventory.instance.IsInventoryFull())
            {
                S_Mold_Inventory.instance.AddToInventory(S_Inventory.instance.GetMaterials());
                S_Inventory.instance.ClearInventory();
                S_UI_Inventory.instance.ClearPlayerInventoryIcon();
               
               
            }
          

            isInInventory = true;
        }
        else
        {
            S_Menu_Manager.Instance.stopPlayer(true);
            mainInventoryGroup.gameObject.SetActive(false);
            reducedInventory.gameObject.SetActive(true);
            isInInventory = false;

            
        }

    }

    public void PressedBakeButton()
    {
        //Lunch timer 
        //add statue to player inventory
        S_Mold_Inventory.instance.CheckRecipeMaterialWithMoldMaterial();
        Debug.Log("blala");
    }
    
}
