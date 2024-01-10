using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Interactable_Obj : S_Interactable
{
    [Header("Display Text")]
    public string description = "";
    public string FullInventoryDescription = "<color=red>INVENTORY IS FULL</color>";

    public S_Materials material;
    public S_DepotRessource depotRessource;

    Transform meshTransform;

    public bool isObj;

    public bool isSelected = false;

    float meshStartScale;
    public float selectScale = 1.5f;
    public float timeToScale = 1;

    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0,0,1,1);

    float scaleT = 0;

    private void Start()
    {
        meshTransform = transform.Find("Mesh");
        meshStartScale = meshTransform.localScale.x;
    }

    private void Update()
    {
        if (scaleT != 1 && isSelected)
        {
            scaleT = Mathf.Clamp(scaleT + Time.deltaTime / timeToScale, 0, 1);
            
            float s = Mathf.Lerp(meshStartScale,selectScale,scaleCurve.Evaluate(scaleT));

            meshTransform.localScale = new Vector3(s, s, s);

        }
        else if (scaleT != 0 && !isSelected)
        {
            scaleT = Mathf.Clamp(scaleT - Time.deltaTime / timeToScale, 0, 1);
            float s = Mathf.Lerp(meshStartScale, selectScale, scaleCurve.Evaluate(scaleT));

            meshTransform.localScale = new Vector3(s, s, s);
        }
    }
    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
            return description;
        }
        else
        {
            return FullInventoryDescription;
        }
    }

    public override string GetMatDescription()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
           return material.description;
        }
        else
        {
            return material.FullInventorydescription;
        }
    }
    public override void Interact()
    {
        if (S_Inventory.instance.GetMaterials() == null)
        {
            isObj = false;
            //S_Player_Interaction.instance.OnInteraction();
            S_Inventory.instance.AddToInventory(material);
            //depotRessource.isObject = false;
            Destroy(gameObject);
        }
    }

}
