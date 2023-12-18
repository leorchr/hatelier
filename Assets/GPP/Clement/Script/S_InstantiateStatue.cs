using System.Collections.Generic;
using UnityEngine;

public class S_InstantiateStatue : MonoBehaviour
{
    public static S_InstantiateStatue instance;
    public Texture2D defaultTexture;
    public List<GameObject> statues = new List<GameObject>();
    public GameObject armaturePrefab;
  

    private void Awake()
    {
        if (!instance) instance = this;
    }
    public void AttributeSpecs(GameObject statue)
    {
        if(S_Statue_Inventory.instance.head != null)
        {
            armaturePrefab = S_Statue_Inventory.instance.head.armature;
        }

        if (S_Statue_Inventory.instance.bottom != null)
        {

            statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_01", S_Statue_Inventory.instance.top.baseColor1);
            statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_01_Normal", S_Statue_Inventory.instance.top.normal1);

            statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_02", S_Statue_Inventory.instance.bottom.baseColor2);
            statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_02_Normal", S_Statue_Inventory.instance.bottom.normal2);

            statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer2_01", S_Statue_Inventory.instance.bottom.baseColor3);
            statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer2_01_Normal", S_Statue_Inventory.instance.bottom.normal3);
        }
        if(S_Statue_Inventory.instance.top != null)
        {
            GameMode.instance.finalStatue = statue;
        }
        else
        {
            GameMode.instance.finalStatue = armaturePrefab;
        }
        
        
    }

    public void ResetText()
    {
        foreach (var item in statues)
        {
            item.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_01", null);
            item.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_01_Normal", null);

            item.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_02", null);
            item.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_02_Normal", null);

            item.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer2_01", null);
            item.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer2_01_Normal", null);

        }
    }
}