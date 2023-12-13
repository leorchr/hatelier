using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Solo_Interact_Mold : S_Interactable
{
    [Header("Display Text")]
    public string description = "";
    public string cannotUseMatText = "<color=red>CANNOT PUT 2 SAME MATERIALS</color>";
    public string inventoryEmpty = "<color=red>CANNOT USE THE MOLD IF NO MATERIAL IN YOUR INVENTORY</color>";
    public string incompatibleRecipe = "<color=red>INCOMPATIBLE RECIPE</color>";
    [Header("Inventory")]
    public GameObject mainInventoryGroup;

    private S_Solo_Mold_Inventory soloMoldInventory;


    public S_Materials mold;

    private void Start()
    {
        soloMoldInventory = GetComponent<S_Solo_Mold_Inventory>();
    }

    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null)
        {
            if (S_Inventory.instance.GetMaterials() == soloMoldInventory.GetMaterial1())
            {
                return cannotUseMatText;
            }
            else if (!CheckPossibleRecipes(S_Inventory.instance.GetMaterials(), soloMoldInventory.GetMaterial1()))
            {
                return incompatibleRecipe;
            }
            else
            {
                return description;
            }
        }
        else if (!soloMoldInventory.IsInventoryFull())
        {
            return inventoryEmpty;
        }
        else if (soloMoldInventory.IsFirstSlotFull())
        {
            return description;
        }
        else { return description; }

    }

    public override string GetMatDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null && S_Inventory.instance.GetMaterials() != soloMoldInventory.GetMaterial1() && CheckPossibleRecipes(S_Inventory.instance.GetMaterials(), soloMoldInventory.GetMaterial1()))
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

        if (S_Inventory.instance.GetMaterials() != soloMoldInventory.GetMaterial1() && CheckPossibleRecipes(S_Inventory.instance.GetMaterials(), soloMoldInventory.GetMaterial1()))
        {

            soloMoldInventory.refreshMoldInv();

            if (S_Inventory.instance.GetMaterials() != null)
                if (!soloMoldInventory.IsInventoryFull() && S_Inventory.instance.GetMaterials().canBeBaked)
                {
                    S_SoundManager.instance.PlaySound(soundType.Crafting_Mat);
                    soloMoldInventory.AddToInventory(S_Inventory.instance.GetMaterials());
                    S_Inventory.instance.ClearInventory();
                    S_UI_Inventory.instance.ClearPlayerInventoryIcon();

                }

            // Quand le ui sera terminer on fera comme ca au lieu du boutton bake
            if (soloMoldInventory.IsInventoryFull())
            {
                soloMoldInventory.CheckRecipeMaterialWithMoldMaterial();

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
        S_Recipes[] availableRecipe = soloMoldInventory.recipesList;
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

}
