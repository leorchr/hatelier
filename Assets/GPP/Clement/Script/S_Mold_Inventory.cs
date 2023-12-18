using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class S_Mold_Inventory : MonoBehaviour
{
    [Header("INVENTORY SLOT")]
    [SerializeField] private S_Materials matOne;
    [SerializeField] private S_Materials matTwo;
    [SerializeField] private S_Materials matThree;

    [Header("UI IMAGE SLOT")]
    [SerializeField] private GameObject moldSlotImage1;
    [SerializeField] private GameObject moldSlotImage2;
    [SerializeField] private GameObject moldSlotImage3;
    [SerializeField] private Sprite defqultSlotImage4;
    [SerializeField] private GameObject uiGroupe;

    [Header("Slider")]
    public GameObject sliderSupport;
    public Slider bakingSlider;
    public float bakingTimer;
    public float addStatueTimer;
    public bool stopTimer = false;

    [Header("Door")]
    public GameObject door;

    [Header("VFX")]
    public VisualEffect vfx;

    public S_Recipes[] recipesList;
    private int recipeNumber;
    private bool launchFunction = false;

    private void Start()
    {
        launchFunction = false;
        bakingSlider.maxValue = bakingTimer;
        bakingSlider.value = bakingTimer;
        if(door != null) door.GetComponent<Animator>().SetBool("GetStatue", false);
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

            if(bakingTimer <= 0)stopTimer = true; 

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
        matTwo = null;
    }

    public void ClearStatueSlot()
    {
        matThree = null;
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
        if (!launchFunction)
        {
            for (int i = 0; i < recipesList.Length; i++)
            {
                if (recipesList[i].requiredMaterials[0] == matOne || recipesList[i].requiredMaterials[1] == matOne)
                {
                    if ((recipesList[i].requiredMaterials[0] == matTwo || recipesList[i].requiredMaterials[1] == matTwo) && matOne != matTwo)
                    {
                        S_SoundManager.instance.PlaySound(soundType.Crafting_Object); S_SoundManager.instance.PlaySound(soundType.Crafting_Object); S_SoundManager.instance.PlaySound(soundType.Crafting_Object);                  
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
    }

    private void AddStatueToMoldInvFunction()
    {
        //Add ui "Baking ..." + Lunch timer
        AddStatueToMoldInv(recipesList[recipeNumber].statue);

        //add statue ui in mold
        //SetStatueIconInMold(recipesList[recipeNumber].statueIcon);

        ClearInventory();

        //VFX
        vfx.Play();


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
        if(S_Objectives.instance != null) S_Objectives.instance.CheckObjective(recipesList[recipeNumber]);
        GameMode.instance.ChangePhase();

        if (door != null) door.GetComponent<Animator>().SetBool("GetStatue", true);
        uiGroupe.SetActive(false);
    }

    public void DisplayMoldInventoryIcons()
    {
        if (GetMaterial1() != null)
        {
            S_Materials moldInventorySlot1 = GetMaterial1();
            moldSlotImage1.GetComponent<Image>().sprite = moldInventorySlot1.icone;
            moldSlotImage1.SetActive(true);
        }

        if (GetMaterial2() != null)
        {
            S_Materials moldInventorySlot2 = GetMaterial2();
            moldSlotImage2.GetComponent<Image>().sprite = moldInventorySlot2.icone;
            moldSlotImage2.SetActive(true);
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
            moldSlotImage1.GetComponent<Image>().sprite = defqultSlotImage4;
        }

        //Refresh second slot
        if (GetMaterial2() != null)
        {
            moldSlotImage2.GetComponent<Image>().sprite = GetMaterial2().icone;
        }
        else
        {
            moldSlotImage2.GetComponent<Image>().sprite = defqultSlotImage4;
        }

        //Refresh third slot
        if (GetMaterial3() != null)
        {
            moldSlotImage3.GetComponent<Image>().sprite = GetMaterial3().icone;
        }
        else
        {
            moldSlotImage3.GetComponent<Image>().sprite = defqultSlotImage4;
        }
    }

    public S_Materials GetMaterial1() { return matOne; }
    public S_Materials GetMaterial2() { return matTwo; }
    public S_Materials GetMaterial3() {  return matThree; }
}
