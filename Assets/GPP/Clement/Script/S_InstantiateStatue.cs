using UnityEngine;

public class S_InstantiateStatue : MonoBehaviour
{
    public static S_InstantiateStatue instance;

  

    private void Awake()
    {
        if (!instance) instance = this;
    }
    public void AttributeSpecs(GameObject statue)
    {
        statue.GetComponent<MeshRenderer>().sharedMaterial = S_Statue_Inventory.instance.top.materials;

        statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_01", S_Statue_Inventory.instance.bottom.baseColor1);
        statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_01_Normal", S_Statue_Inventory.instance.bottom.normal1);

        //statue.GetComponent<MeshRenderer>().material.SetTexture("_Layer2_01", S_Statue_Inventory.instance.bottom.baseColor2);
        //statue.GetComponent<MeshRenderer>().material.SetTexture("_Layer2_02_Normal", S_Statue_Inventory.instance.bottom.normal2);

        

    }

    public void InstantiateStatue(GameObject statue)
    {

        Instantiate(statue);
        //add component, script de clarence bouger la statue
    }
}
