using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class S_Solo_Mold_Inventory : MonoBehaviour
{
    [Header("INVENTORY SLOT")]
    [SerializeField] private S_Materials matOne;
    [SerializeField] private S_Materials matThree;

    [Header("UI IMAGE SLOT")]
    [SerializeField] private GameObject moldSlotImage1;
    [SerializeField] private GameObject moldSlotImage3;

    [Header("Slider")]
    public GameObject sliderSupport;
    public Slider bakingSlider;
    public float bakingTimer;
    public float addStatueTimer;
    public bool stopTimer = false;

    [Header("Door")]
    public GameObject door;


    public S_Recipes[] recipesList;
    private int recipeNumber;
    private bool launchFunction = false;

    private void Start()
    {
        launchFunction = false;
        bakingSlider.maxValue = bakingTimer;
        bakingSlider.value = bakingTimer;
    }
    public void AddToInventory(S_Materials material)
    {
            matOne = material;
        DisplayMoldInventoryIcons();
    }

    public void StartTimer()
    {
        StartCoroutine(StartTimerTicker());
    }

    IEnumerator StartTimerTicker()
    {
        while (stopTimer == false)
        {
            bakingTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (bakingTimer <= 0) stopTimer = true;

            if (stopTimer == false) bakingSlider.value = bakingTimer;

        }
        sliderSupport.SetActive(false);
    }

    private void AddStatueToMoldInv(S_Materials material)
    {
        matThree = material;
    }
    public void ClearInventory()
    {
        matOne = null;
    }

    public void ClearStatueSlot()
    {
        matThree = null;
    }

    public bool IsInventoryFull()
    {
        return matOne != null;
    }

    public bool IsFirstSlotFull()
    {
        return matOne != null;
    }

    public void CheckRecipeMaterialWithMoldMaterial()
    {
        if (!launchFunction)
        {
            for (int i = 0; i < recipesList.Length; i++)
            {
                if (recipesList[i].requiredMaterials[0] == matOne || recipesList[i].requiredMaterials[1] == matOne)
                {
                    
                    
                        sliderSupport.SetActive(true);
                        recipeNumber = i;
                        //clear mold inventory
                        launchFunction = true;
                        Invoke("AddStatueToMoldInvFunction", bakingTimer);
                        StartTimer();
                    
                }
            }
        }
    }

    private void AddStatueToMoldInvFunction()
    {
        //Add ui "Baking ..." + Lunch timer
        AddStatueToMoldInv(recipesList[recipeNumber].statue);

        //add statue ui in mold
        //SetStatueIconInMold(recipesList[recipeNumber].statueIcon);

        ClearInventory();


        //clear mold ui
        refreshMoldInv();

        Invoke("AddStatueToStatueInvFunction", addStatueTimer);
    }

    private void AddStatueToStatueInvFunction()
    {

        ClearMoldInventoryStatueIcon();
        ClearStatueSlot();

        //add statue to statue inventory 
        S_Statue_Inventory.instance.AddToInventory(recipesList[recipeNumber].statue);

        //ouvrir porte
        //score

    }

    public void DisplayMoldInventoryIcons()
    {
        if (GetMaterial1() != null)
        {
            S_Materials moldInventorySlot1 = GetMaterial1();
            moldSlotImage1.GetComponent<Image>().sprite = moldInventorySlot1.icone;
            moldSlotImage1.SetActive(true);
        }

    }

    public void ClearMoldInventoryStatueIcon()
    {
        moldSlotImage3.SetActive(false);

    }

    public void SetStatueIconInMold(Sprite statueIcon)
    {
        moldSlotImage3.GetComponent<Image>().sprite = statueIcon;
    }

    public void refreshMoldInv()
    {
        //Refresh first slot
        if (GetMaterial1() != null)
        {
            moldSlotImage1.GetComponent<Image>().sprite = GetMaterial1().icone;
        }
        else
        {
            moldSlotImage1.GetComponent<Image>().sprite = null;
        }

        //Refresh third slot
        if (GetMaterial3() != null)
        {
            moldSlotImage3.GetComponent<Image>().sprite = GetMaterial3().icone;
        }
        else
        {
            moldSlotImage3.GetComponent<Image>().sprite = null;
        }


    }

    public S_Materials GetMaterial1() { return matOne; }
    public S_Materials GetMaterial3() { return matThree; }
}