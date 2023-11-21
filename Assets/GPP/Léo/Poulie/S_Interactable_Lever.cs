using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class S_Interactable_Lever : S_Interactable
{
    [Header("Display Text")]
    [SerializeField] private string description = "Press <color=red>RIGHT CLICK</color>";
    [SerializeField] private S_MovingObject[] moveObjects;
    private bool canInteract;

    public override void Interact()
    {
        foreach (S_MovingObject moveObject in moveObjects)
        {
            canInteract = true;
            if(moveObject.positionState == S_MovingObject.PoulieState.Moving)
            {
                canInteract = false;
                break;
            }
        }

        if(canInteract)
        {
            foreach (S_MovingObject moveObject in moveObjects) {
                moveObject.Interact();
            }
        }
    }

    public override string GetDescription()
    {
        return description;
    }

    public override string GetMatDescription()
    {
        return "";
    }
}

