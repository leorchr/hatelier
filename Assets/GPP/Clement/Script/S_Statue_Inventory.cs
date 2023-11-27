using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Statue_Inventory : MonoBehaviour
{
    [SerializeField] private S_Materials head;
    [SerializeField] private S_Materials top;
    [SerializeField] private S_Materials bottom;

  public void AddToInventory(S_Materials material)
   {
        if(head != null)
        {
            top = material;
        }
        else
        {
            head = material;
        }
        if(head != null && top != null)
        {
            bottom = material;
        }

   }

    public void RemoveFromInventory(S_Materials material)
    {
        head = null;
        top = null;
        bottom = null;
    }



}

