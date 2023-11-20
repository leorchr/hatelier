using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class S_Inventory : MonoBehaviour
{
    public static S_Inventory instance;

    [SerializeField] private S_Materials inventory;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void AddToInventory(S_Materials material)
    {
        inventory = material;
        S_UI_Inventory.instance.DisplayIcon();
    }

    public void ClearInventory()
    {
        inventory = null;
    }

    public S_Materials GetMaterials() { return inventory; }
}