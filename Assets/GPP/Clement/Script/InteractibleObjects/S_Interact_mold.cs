using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Interact_mold : S_Interactable
{
    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";
    public string cannotUseMatText = "<color=red>CANNOT PUT 2 SAME MATERIALS</color>";
    public string inventoryEmpty = "<color=red>CANNOT USE THE MOLD IF NO MATERIAL IN YOUR INVENTORY</color>";
    [Header("Inventory")]
    public Image reducedInventory;
    public GameObject mainInventoryGroup;

    public S_Materials mold;
    private bool isInInventory = false;

    private void Update()
    {
        Debug.Log(isInInventory);
    }

    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null)
        {
            if (S_Inventory.instance.GetMaterials() != S_Mold_Inventory.instance.GetMaterial1())
            {
                return description;
            }
            else
            {
                return cannotUseMatText;
            }
        }
        else if(!S_Mold_Inventory.instance.IsInventoryFull())
        {
                return inventoryEmpty;
        }
        else if (S_Mold_Inventory.instance.IsFirstSlotFull())
        {
            return description;
        }
        else { return description; }
           
    }

    public override string GetMatDescription()
    {
        if(S_Inventory.instance.GetMaterials() != S_Mold_Inventory.instance.GetMaterial1())
        {
            return mold.description;

        }
        else
        {
            return null;
        }
    }

    public override void Interact()
    {
        
        if (S_Inventory.instance.GetMaterials() != S_Mold_Inventory.instance.GetMaterial1())
        {

            if (isInInventory == false)
            {
                S_Menu_Manager.Instance.stopPlayer(false);
                mainInventoryGroup.gameObject.SetActive(true);
                reducedInventory.gameObject.SetActive(false);

                if(S_Inventory.instance.GetMaterials() != null)
                if (!S_Mold_Inventory.instance.IsInventoryFull() && S_Inventory.instance.GetMaterials().canBeBaked)
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
                new WaitForSeconds(3);
                //S_UI_Inventory.instance.ClearMoldInventoryStatueIcon();
                isInInventory = false;


            }
        }
       
        

        

    }

    public void PressedBakeButton()
    {
        //Lunch timer 
        //add statue to player inventory
        S_Mold_Inventory.instance.CheckRecipeMaterialWithMoldMaterial();
        
    }
    
}
