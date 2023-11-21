using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Inventory : MonoBehaviour
{

    public static S_UI_Inventory instance;
    [SerializeField] private GameObject moldSlotImage1;
    [SerializeField] private GameObject moldSlotImage2;
    [SerializeField] private GameObject playerInventoryImage;
    [SerializeField] private GameObject bakeButton;

    private void Awake()

    {
        if (!instance) instance = this;
    }

    public void DisplayPlayerInventoryIcon()
    {
        S_Materials playerInventory = S_Inventory.instance.GetMaterials();
     

        playerInventoryImage.GetComponent<Image>().sprite = playerInventory.icone;
      

        playerInventoryImage.SetActive(true);
      

    }

    public void DisplayMoldInventoryIcons()
    {
        if(S_Mold_Inventory.instance.GetMaterial1() != null)
        {
            S_Materials moldInventorySlot1 = S_Mold_Inventory.instance.GetMaterial1();
            moldSlotImage1.GetComponent<Image>().sprite = moldInventorySlot1.icone;
            moldSlotImage1.SetActive(true);
        }

        if (S_Mold_Inventory.instance.GetMaterial2() != null)
        {
            S_Materials moldInventorySlot2 = S_Mold_Inventory.instance.GetMaterial2();
            moldSlotImage2.GetComponent<Image>().sprite = moldInventorySlot2.icone;
            moldSlotImage2.SetActive(true);
        }

    }

    public void DisplayBakeButton()
    {
        bakeButton.SetActive(true);
    }

    public void ClearPlayerInventoryIcon()
    {
        playerInventoryImage.SetActive(false);
    }

    public void ClearMoldInventoryIcon()
    {
        moldSlotImage1.SetActive(false);
        moldSlotImage2.SetActive(false);
    }
}