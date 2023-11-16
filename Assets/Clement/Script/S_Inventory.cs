using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_Inventory : MonoBehaviour
{
    public static S_Inventory Instance;

    [SerializeField] private List<S_Materials> materials = new List<S_Materials>();

    private void Start()
    {
        Instance = this;
    }

    public void AddToInventory(S_Materials material)
    {
        materials.Add(material);
    }

    public void RemoveFromInventory(S_Materials material)
    {
        materials.Remove(material);
    }
}