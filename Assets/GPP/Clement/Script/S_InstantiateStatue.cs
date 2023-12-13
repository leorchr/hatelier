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

        statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_02", S_Statue_Inventory.instance.bottom.baseColor2);
        statue.GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_Layer1_02_Normal", S_Statue_Inventory.instance.bottom.normal2);

        GameMode.instance.finalStatue = statue;
        GameMode.instance.finalStatue.AddComponent<S_SculptRotate>();
    }

}
