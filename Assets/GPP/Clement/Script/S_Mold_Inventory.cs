using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Mold_Inventory : MonoBehaviour
{
    [SerializeField] private S_Materials matOne;
    [SerializeField] private S_Materials matTwo;
    [SerializeField] private S_Materials matThree;

    [SerializeField] private S_Recipes[] recipesList;
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

    public bool IsFirstSlotFull()
    {
        return matOne != null;
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
                    //Add ui "Baking ..." + Lunch timer
                    AddToInventory(recipesList[i].statue);

                    //add statue to statue inventory 
                    S_Statue_Inventory.instance.AddToInventory(recipesList[i].statue);

                    //clear mold inventory
                    ClearInventory();

                    //clear mold ui
                    S_UI_Inventory.instance.refreshMoldInv();

                    //add statue ui in mold
                    S_UI_Inventory.instance.SetStatueIconInMold(recipesList[i].statueIcon);

                    break;
                }
            }
        }
    }
    public S_Materials GetMaterial1() { return matOne; }
    public S_Materials GetMaterial2() { return matTwo; }
    public S_Materials GetMaterial3() {  return matThree; }
}
