using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Player_Interaction : MonoBehaviour
{

    public static S_Player_Interaction instance;
    S_Interactable interactable;

    [Header("Text")]
    public TMPro.TextMeshProUGUI interactionText;
    public TMPro.TextMeshProUGUI materialDescriptionText;
    public LayerMask interactableLayer;


    private void Awake()
    {
        if(!instance) instance = this;
    }
   
    void Update()
    {
       
        if (Input.GetMouseButtonDown(1))
        {
            if (interactable)
            {
                HandleInteraction();
            }
        }

    }

   public void OnTriggerEnter(Collider collider)
    {
        Debug.Log(interactableLayer.value);
        if ((1 << collider.gameObject.layer & interactableLayer.value) != 0)
        {
            Debug.Log("COUCOU JE SUIS ANSNAJSNAS");

            interactable = (S_Interactable)collider.GetComponent(typeof(S_Interactable));

            if (interactable != null)
            {
                interactionText.text = interactable.GetDescription();
                interactionText.gameObject.SetActive(true);

                materialDescriptionText.text = interactable.GetMatDescription();
                materialDescriptionText.gameObject.SetActive(true);
            }
            if (interactable == null)
            {
                interactionText.gameObject.SetActive(false);
                materialDescriptionText.gameObject.SetActive(false);
            }
        }
    }




    public void OnTriggerExit(Collider other)
    {
        OnInteraction();
    }

    public void OnInteraction()
    {
        interactable = null;
        interactionText.gameObject.SetActive(false);
        materialDescriptionText.gameObject.SetActive(false);
    }


    void HandleInteraction()
    {
        switch (interactable.interactiontype)
        {
            case S_Interactable.InteractionType.Click:
                interactable.Interact();
                break;
        }
    }
}


