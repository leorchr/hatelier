using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Statue_Inventory : MonoBehaviour
{
    public static S_Statue_Inventory instance;

    public S_Materials head;
    public S_Materials top;
    public S_Materials bottom;
    [SerializeField] private GameObject statue;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void AddToInventory(S_Materials material)
   {
        if(head == null)
        {
            head = material;
        }
        else if(top == null)
        {
            top = material;
        }
        else if(bottom == null)
        {
            bottom = material;
        }
        S_UI_Inventory.instance.DisplayStatueInventory();
        statue = head.prefab;
       
   }

    public void InstantiateStatue()
    {
        if(head != null && top != null && bottom != null)
        {
            S_InstantiateStatue.instance.AttributeSpecs(statue);
            S_InstantiateStatue.instance.InstantiateStatue();

        }
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

