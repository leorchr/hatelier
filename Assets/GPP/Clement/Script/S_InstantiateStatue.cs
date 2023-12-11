using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class S_InstantiateStatue : MonoBehaviour
{
    public static S_InstantiateStatue instance;

    [SerializeField] private Texture2D baseColor1;
    [SerializeField] private Texture2D normal1;

    [SerializeField] private Texture2D baseColor2;
    [SerializeField] private Texture2D normal2;

    private void Awake()
    {
        instance = this;
    }
    public void AttributeSpecs(GameObject statue)
    {
        statue.GetComponent<MeshRenderer>().material = S_Statue_Inventory.instance.top.materials;

        statue.GetComponent<MeshRenderer>().material.SetTexture("_Layer1_01", baseColor1);
        statue.GetComponent<MeshRenderer>().material.SetTexture("_Layer1_01_Normal", normal1);

        statue.GetComponent<MeshRenderer>().material.SetTexture("_Layer2_01", baseColor1);
        statue.GetComponent<MeshRenderer>().material.SetTexture("_Layer2_02_Normal", baseColor1);

        //couleur

    }

    public void InstantiateStatue(GameObject statue)
    {
        Instantiate(statue);
    }
}
