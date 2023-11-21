using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Mold_Inventory : MonoBehaviour
{
    public static S_Mold_Inventory instance;

    [SerializeField] private S_Materials matOne;
    [SerializeField] private S_Materials matTwo;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void AddToInventory(S_Materials material)
    {

        if (matOne != null)
        {
            matTwo = material;
        }
        else
        {
            matOne = material;
        }
        
        S_UI_Inventory.instance.DisplayMoldInventoryIcons();
        
    }



    public void ClearInventory()
    {
        matOne = null;
        matTwo = null;
    }

    public bool IsInventoryFull()
    {
        return matOne != null && matTwo != null;
    }

    public S_Materials GetMaterial1() { return matOne; }
    public S_Materials GetMaterial2() { return matTwo; }

}
