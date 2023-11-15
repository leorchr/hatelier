using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Exemple_Interactable_Obj : S_Interactable
{
    private string description = " press <color=red>RIGHT CLICK</color>";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override string GetDescription()
    {
        return description;
    }

    public override void Interact()
    {
        S_Player_Interaction.instance.OnInteraction();
        Destroy(gameObject);
        //Add cube to inventori
    }


}
