using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_Inventory : MonoBehaviour
{
    public static S_Inventory instance;

    [SerializeField] private S_Materials inventory;

    public Transform dropPoint;

    public GameObject dropButton;
    private void Awake()
    {
        if (!instance) instance = this;
      
    }

    private void Start()
    {
        dropButton.SetActive(false);
    }

    private void Update()
    {
        if (inventory != null)
        {
            dropButton?.SetActive(true);
            S_UI_Inventory.instance.inventoryGroupe.SetActive(true);
            if (S_Objectives.instance.checkCurrentMaterial(inventory))
            {
                MissionWaypoint.instance.HighOpacity();
            }
            else
            {
                MissionWaypoint.instance.LowOpacity();
            }
        }
        else
        {
            dropButton.SetActive(false);
            S_UI_Inventory.instance.inventoryGroupe.SetActive(false);
            
        }
    }

    public void AddToInventory(S_Materials material)
    {
        S_SoundManager.instance.PlaySound(soundType.Stuff_Object);
        GetComponent<S_Player_Anim_Manager>().setPickUp(true);
        inventory = material;
        S_UI_Inventory.instance.DisplayPlayerInventoryIcon();
    }

    public void ClearInventory()
    {
        Invoke("InvokeLowOp", 1.2f);
        inventory = null;
    }

    public void DropItem()
    {
       
        if (inventory == null) return;
        if (inventory.prefab == null) { Debug.LogError("Prefab need to be asigned to the scriptable object"); return; }
    
        GameObject item = Instantiate(inventory.prefab, dropPoint);
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        
        ClearInventory();
        S_SoundManager.instance.PlaySound(soundType.Drop_object);
        S_UI_Inventory.instance.ClearPlayerInventoryIcon();
        
    }

    public void InvokeLowOp()
    {
        MissionWaypoint.instance.LowOpacity();
    }

    public S_Materials GetMaterials() { return inventory; }
}