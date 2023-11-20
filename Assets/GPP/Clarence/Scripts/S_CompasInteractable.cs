using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CompasInteractable : S_Interactable
{
    [Header("Display Text")]
    private string description = "Press <color=red>RIGHT CLICK</color>";

    private GameObject[] BasePlate = new GameObject[2];

    public override string GetDescription()
    {
        return description;
    }

    public override string GetMatDescription()
    {
        return "";

    }
    public override void Interact()
    {
        S_Player_Interaction.instance.OnInteraction();

        //Add cube to inventory
        Destroy(gameObject);
    }

}
