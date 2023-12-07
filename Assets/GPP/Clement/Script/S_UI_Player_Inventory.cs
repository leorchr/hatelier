using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Player_Inventory : MonoBehaviour
{
    [SerializeField] private Image itemSlot;

    // Update is called once per frame
    void Update()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}
