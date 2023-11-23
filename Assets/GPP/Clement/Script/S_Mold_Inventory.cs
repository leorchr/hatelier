using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Mold_Inventory : MonoBehaviour
{
    public static S_Mold_Inventory instance;

    [SerializeField] private S_Materials matOne;
    [SerializeField] private S_Materials matTwo;

    [SerializeField] private S_Recipes[] recipesList;

  
    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void AddToInventory(S_Materials material)
    {
       
        if (matOne != null)
        {
            matTwo = material;
        }
        else
        {
            matOne = material;
        }
        
        S_UI_Inventory.instance.DisplayMoldInventoryIcons();
        
    }
    public void ClearInventory()
    {
        matOne = null;
        matTwo = null;
    }

    public bool IsInventoryFull()
    {
        return matOne != null && matTwo != null;
    }

    public bool IsSameMaterial()
    {
        return matOne == matTwo;
    }

    public void CheckRecipeMaterialWithMoldMaterial()
    {
        for (int i = 0; i < recipesList.Length; i++)
        {
            if (recipesList[i].requiredMaterials[0] == matOne || recipesList[i].requiredMaterials[1] == matOne)
            {
                if((recipesList[i].requiredMaterials[0] == matTwo || recipesList[i].requiredMaterials[1] == matTwo) && matOne != matTwo)
                {
                    
                    Debug.Log("corresponding");
                    //Add ui "Baking ..." + Lunch timer

                    //add statue ui in mold
                    S_UI_Inventory.instance.SetStatueIconInMold(recipesList[i].statueIcon);
                    //add statue 
                    S_Inventory.instance.AddToInventory(recipesList[i].statue);

                    //clear inventory
                    ClearInventory();
                    //clear mold 
                    S_UI_Inventory.instance.ClearMoldInventoryIcon();
                    break;


                }
                else
                {

                }





            }

        }
    }



    public S_Materials GetMaterial1() { return matOne; }
    public S_Materials GetMaterial2() { return matTwo; }


}
