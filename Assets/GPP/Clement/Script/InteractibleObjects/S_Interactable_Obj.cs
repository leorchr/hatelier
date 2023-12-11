using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Interactable_Obj : S_Interactable
{
    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";
    public string FullInventoryDescription = "<color=red>INVENTORY IS FULL</color>";

    public S_Materials material;
    public S_DepotRessource depotRessource;

    public bool isObj;

    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
            return description;
        }
        else
        {
            return FullInventoryDescription;
        }
    }

    public override string GetMatDescription()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
           return material.description;
        }
        else
        {
            return material.FullInventorydescription;
        }
    }
    public override void Interact()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
            isObj = false;
            S_Player_Interaction.instance.OnInteraction();
            S_Inventory.instance.AddToInventory(material);
            //depotRessource.isObject = false;
            Destroy(gameObject);
        }
    }

}
