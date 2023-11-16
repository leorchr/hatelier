using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Interactable_Obj : S_Interactable
{
    [Header("Display Text")]


    public string description = "Press <color=red>RIGHT CLICK</color>";

    public S_Materials material;

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
        S_Player_Interaction.instance.OnInteraction();

        //Add cube to inventory
        S_Inventory.instance.AddToInventory(material);
        Destroy(gameObject);
    }



}
