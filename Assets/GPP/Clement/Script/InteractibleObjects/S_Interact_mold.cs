using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Interact_mold : S_Interactable
{
    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";
    public string cannotUseMatText = "<color=red>CANNOT PUT 2 SAME MATERIALS</color>";
    public string inventoryEmpty = "<color=red>CANNOT USE THE MOLD IF NO MATERIAL IN YOUR INVENTORY</color>";
    public string incompatibleRecipe = "<color=red>INCOMPATIBLE RECIPE</color>";
    [Header("Inventory")]
    private Image reducedInventory;
    public GameObject mainInventoryGroup;

    private S_Mold_Inventory moldInventory;

    public S_Materials mold;

    private void Start()
    {
        reducedInventory = S_UI_Inventory.instance.GetComponent<Image>();
        moldInventory = GetComponent<S_Mold_Inventory>();
    }

    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null)
        {
            if (S_Inventory.instance.GetMaterials() == moldInventory.GetMaterial1())
            {
                return cannotUseMatText;
            }
            else if (!CheckPossibleRecipes(S_Inventory.instance.GetMaterials(),moldInventory.GetMaterial1()))
            {
                return incompatibleRecipe;
            }
            else
            {
                return description;
            }
        }
        else if(!moldInventory.IsInventoryFull())
        {
                return inventoryEmpty;
        }
        else if (moldInventory.IsFirstSlotFull())
        {
            return description;
        }
        else { return description; }
           
    }

    public override string GetMatDescription()
    {
        
        if (S_Inventory.instance.GetMaterials() != null && S_Inventory.instance.GetMaterials() != moldInventory.GetMaterial1() && CheckPossibleRecipes(S_Inventory.instance.GetMaterials(), moldInventory.GetMaterial1()))
        {
            return mold.description;

        }
        else
        {
            return null;
        }
    }

    public override void Interact()
    {
        
        if (S_Inventory.instance.GetMaterials() != moldInventory.GetMaterial1() && CheckPossibleRecipes(S_Inventory.instance.GetMaterials(), moldInventory.GetMaterial1()))
        {

                moldInventory.refreshMoldInv();
            S_SoundManager.instance.PlaySound(soundType.Crafting_Mat);
                if(S_Inventory.instance.GetMaterials() != null)
                if (!moldInventory.IsInventoryFull() && S_Inventory.instance.GetMaterials().canBeBaked)
                {

                    moldInventory.AddToInventory(S_Inventory.instance.GetMaterials());
                    S_Inventory.instance.ClearInventory();
                    S_UI_Inventory.instance.ClearPlayerInventoryIcon();
                    
                }

                // Quand le ui sera terminer on fera comme ca au lieu du boutton bake
                if (moldInventory.IsInventoryFull())
                {
                    moldInventory.CheckRecipeMaterialWithMoldMaterial();
                    
                 }
               
                //isInInventory = true;
            //}
            //else
            //{
               // S_Menu_Manager.Instance.stopPlayer(true);
                //mainInventoryGroup.gameObject.SetActive(false);
                //reducedInventory.gameObject.SetActive(true);
                //S_UI_Inventory.instance.ClearMoldInventoryStatueIcon();
                //isInInventory = false;
                //BakeButton.onClick.RemoveAllListeners();

            //}
        }
       
        

        

    }

    private bool CheckPossibleRecipes(S_Materials m1, S_Materials m2)
    {
        S_Recipes[] availableRecipe = moldInventory.recipesList;
        bool isSoloMold = false;
        if (availableRecipe.Length > 0)
        {
            isSoloMold = availableRecipe[0].requiredMaterials.Length == 1;
        }
        else
        {
            return false;
        }
        if (isSoloMold)
        {
            foreach (S_Recipes r in availableRecipe)
            {
                if (r.requiredMaterials[0] == m1)
                {
                    return true;
                }
            }
        }
        else
        {
            if (m2 == null)
            {
                foreach (S_Recipes r in availableRecipe)
                {
                    foreach (S_Materials rm in r.requiredMaterials)
                    {
                        if (rm == m1)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                List<S_Recipes> recipeCompatible = new List<S_Recipes>();
                List<int> materialCompatiblePos = new List<int>();
                foreach (S_Recipes r in availableRecipe)
                {
                    for (int i = 0; i < r.requiredMaterials.Length; i++)
                    {
                        if (r.requiredMaterials[i] == m2)
                        {
                            recipeCompatible.Add(r);
                            materialCompatiblePos.Add(i);

                        }
                    }
                }
                for (int i = 0; i < recipeCompatible.Count; i++)
                {
                    if (recipeCompatible[i].requiredMaterials[1 - materialCompatiblePos[i]] == m1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        return false;
    }

}
