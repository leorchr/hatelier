using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Interact_Lever : S_Interactable
{

    public string description = "Press <color=red>RIGHT CLICK</color>";
    public string noMatDescription;
    public S_TourneDisque tourneDisque;

    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
            return description;
        }
        else
        {
            return description;
        }
    }

    public override string GetMatDescription()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
            return noMatDescription;
        }
        else
        {
            return noMatDescription;
        }
    }
    public override void Interact()
    {
        Debug.Log("interact");
        if (S_Inventory.instance.GetMaterials() == null)
        {
            if (tourneDisque.turnToLeft)
            {
                tourneDisque.turnToLeft = false;
            }
            else
            {
                tourneDisque.turnToLeft = true;
            }
        }
    }
}
