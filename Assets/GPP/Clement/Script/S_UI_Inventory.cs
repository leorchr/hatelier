using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Inventory : MonoBehaviour
{

    public static S_UI_Inventory instance;

    [Header("Statue UI slots")]
    [SerializeField] private GameObject statueSlotImage1;
    [SerializeField] private GameObject statueSlotImage2;
    [SerializeField] private GameObject statueSlotImage3;
    [SerializeField] private Sprite baseStatueUI;
    [SerializeField] private GameObject playerInventoryImage;
    public GameObject inventoryGroupe;

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
            statueSlotImage1.GetComponent<ShowSculpturePart>().setImage(statueHead.icone);
            statueSlotImage1.SetActive(true);

        }
        else
        {
            statueSlotImage1.GetComponent<Image>().sprite = baseStatueUI;
        }
        if (S_Statue_Inventory.instance.GetMaterial2() != null)
        {
            S_Materials statueTop = S_Statue_Inventory.instance.GetMaterial2();
            statueSlotImage2.GetComponent<ShowSculpturePart>().setImage(statueTop.icone);
            statueSlotImage2.SetActive(true);

        }
        else
        {
            statueSlotImage2.GetComponent<Image>().sprite = baseStatueUI;
        }
        if (S_Statue_Inventory.instance.GetMaterial3() != null)
        {
            S_Materials statueBottom = S_Statue_Inventory.instance.GetMaterial3();
            statueSlotImage3.GetComponent<ShowSculpturePart>().setImage(statueBottom.icone);
            statueSlotImage3.SetActive(true);

        }
        else
        {
            statueSlotImage3.GetComponent<Image>().sprite = baseStatueUI;
        }

    }

    public void ClearPlayerInventoryIcon()
    {
        playerInventoryImage.SetActive(false);
    }

   
    public void SetMoldInventory(S_Mold_Inventory smi)
    {
        currentMoldInv = smi;
    }

    


}