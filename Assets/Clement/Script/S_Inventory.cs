using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class S_Inventory : MonoBehaviour
{
    public static S_Inventory instance;

    [SerializeField] private List<S_Materials> materials = new List<S_Materials>();

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void AddToInventory(S_Materials material)
    {
        materials.Add(material);
        S_UI_Inventory.instance.DisplayIcon();
    }

    public void RemoveFromInventory(S_Materials material)
    {
        materials.Remove(material);
    }

    public List<S_Materials> GetMaterials() { return materials; }
}