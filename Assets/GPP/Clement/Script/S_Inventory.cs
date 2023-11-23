using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class S_Inventory : MonoBehaviour
{
    public static S_Inventory instance;

    [SerializeField] private S_Materials inventory;

    public Transform dropPoint;
    private void Awake()
    {
        if (!instance) instance = this;
      
    }

    public void AddToInventory(S_Materials material)
    {
        inventory = material;
        S_UI_Inventory.instance.DisplayPlayerInventoryIcon();
    }

    public void ClearInventory()
    {
        inventory = null;
        Debug.Log("cleared");
    }

    public void DropItem()
    {
       
        if (inventory == null) return;
        if (inventory.prefab == null) { Debug.LogError("Prefab need to be asigned to the scriptable object"); return; }
    
        GameObject item = Instantiate(inventory.prefab, dropPoint);
        item.transform.parent = null;
        
        ClearInventory();
        S_UI_Inventory.instance.ClearPlayerInventoryIcon();
        
    }

    public S_Materials GetMaterials() { return inventory; }
}