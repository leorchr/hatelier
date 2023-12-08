using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class S_InstantiateStatue : MonoBehaviour
{
    public static S_InstantiateStatue instance;
    private void Awake()
    {
        instance = this;
    }
    public void AttributeSpecs(GameObject statue)
    {
        statue.GetComponent<MeshRenderer>().material = S_Statue_Inventory.instance.top.materials;
        //couleur
        
    }

    public void InstantiateStatue()
    {

    }
}
