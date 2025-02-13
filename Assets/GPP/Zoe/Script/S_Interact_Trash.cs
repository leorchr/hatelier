using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Interact_Trash : S_Interactable
{
    public string description = "";
    public string trashDescription;
    public string nothingToDestroy;
    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null)
        {
            return description;
        }
        else
        {
            return nothingToDestroy;
        }
    }

    public override string GetMatDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null)
        {
            return trashDescription;
        }
        else
        {
            return nothingToDestroy;
        }
    }
    public override void Interact()
    {
        S_Inventory.instance.ClearInventory();
        S_SoundManager.instance.PlaySound(soundType.Bin);
        S_UI_Inventory.instance.ClearPlayerInventoryIcon();
    }
}
