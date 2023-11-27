using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Inventory : MonoBehaviour
{

    public static S_UI_Inventory instance;
    [SerializeField] private GameObject moldSlotImage1;
    [SerializeField] private GameObject moldSlotImage2;
    [SerializeField] private GameObject moldSlotImage3;
    [SerializeField] private GameObject statueSlotImage1;
    [SerializeField] private GameObject statueSlotImage2;
    [SerializeField] private GameObject statueSlotImage3;
    [SerializeField] private GameObject playerInventoryImage;
    [SerializeField] private GameObject bakeButton;

    private S_Mold_Inventory currentMoldInv = null;
    

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

    public void DisplayStatueInventory()
    {
        if(S_Statue_Inventory.instance.GetMaterial1() != null)
        {
            S_Materials statueHead = S_Statue_Inventory.instance.GetMaterial1();
            statueSlotImage1.GetComponent<Image>().sprite = statueHead.icone;
            statueSlotImage1.SetActive(true);

        }
        if (S_Statue_Inventory.instance.GetMaterial2() != null)
        {
            S_Materials statueTop = S_Statue_Inventory.instance.GetMaterial2();
            statueSlotImage2.GetComponent<Image>().sprite = statueTop.icone;
            statueSlotImage2.SetActive(true);

        }
        if (S_Statue_Inventory.instance.GetMaterial3() != null)
        {
            S_Materials statueBottom = S_Statue_Inventory.instance.GetMaterial3();
            statueSlotImage3.GetComponent<Image>().sprite = statueBottom.icone;
            statueSlotImage3.SetActive(true);

        }

    }
    public void DisplayMoldInventoryIcons()
    {
        if(currentMoldInv.GetMaterial1() != null)
        {
            S_Materials moldInventorySlot1 = currentMoldInv.GetMaterial1();
            moldSlotImage1.GetComponent<Image>().sprite = moldInventorySlot1.icone;
            moldSlotImage1.SetActive(true);
        }

        if (currentMoldInv.GetMaterial2() != null)
        {
            S_Materials moldInventorySlot2 = currentMoldInv.GetMaterial2();
            moldSlotImage2.GetComponent<Image>().sprite = moldInventorySlot2.icone;
            moldSlotImage2.SetActive(true);
            if(!currentMoldInv.IsSameMaterial()) DisplayBakeButton();


        }
        else
        {
            DisableButton();
        }


    }

   

    public void DisplayBakeButton()
    {
        bakeButton.SetActive(true);
    }

    public void DisableButton()
    {
        bakeButton.SetActive(false);

    }

    public void ClearPlayerInventoryIcon()
    {
        playerInventoryImage.SetActive(false);
    }

    public void ClearMoldInventoryStatueIcon()
    {
        moldSlotImage3.SetActive(false);
        
    }

    public void SetStatueIconInMold( Sprite statueIcon)
    {
        moldSlotImage3.GetComponent<Image>().sprite = statueIcon;
    }

    public void SetMoldInventory(S_Mold_Inventory smi)
    {
        currentMoldInv = smi;
    }

    public void refreshMoldInv()
    {
        //Refresh first slot
        if (currentMoldInv.GetMaterial1() != null) 
        {
            moldSlotImage1.GetComponent<Image>().sprite = currentMoldInv.GetMaterial1().icone;
        }
        else
        {
            moldSlotImage1.GetComponent<Image>().sprite = null;
        }

        //Refresh second slot
        if (currentMoldInv.GetMaterial2() != null)
        {
            moldSlotImage2.GetComponent<Image>().sprite = currentMoldInv.GetMaterial2().icone;
        }
        else
        {
            moldSlotImage2.GetComponent<Image>().sprite = null;
        }

        //Refresh third slot
        if (currentMoldInv.GetMaterial3() != null) 
        {
            moldSlotImage3.GetComponent<Image>().sprite = currentMoldInv.GetMaterial3().icone;
        }
        else
        {
            moldSlotImage3.GetComponent<Image>().sprite = null;
        }

        //Refresh Bake button
        if (currentMoldInv.GetMaterial1() != null && currentMoldInv.GetMaterial2() != null)
        {
            DisplayBakeButton();
        }
        else
        {
            DisableButton();
        }
    }
}