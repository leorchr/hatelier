using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Statue_Inventory : MonoBehaviour
{
    public static S_Statue_Inventory instance;

    [SerializeField] private S_Materials head;
    [SerializeField] private S_Materials top;
    [SerializeField] private S_Materials bottom;

    private void Awake()
    {
        if (!instance) instance = this;
    }
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
        S_UI_Inventory.instance.DisplayStatueInventory();
   }

    public void RemoveFromInventory(S_Materials material)
    {
        head = null;
        top = null;
        bottom = null;
    }


    public S_Materials GetMaterial1() { return head; }
    public S_Materials GetMaterial2() { return top; }
    public S_Materials GetMaterial3() { return bottom; }
}

