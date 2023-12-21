using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class S_Player_Interaction : MonoBehaviour
{
    public static S_Player_Interaction instance;
    S_Interactable interactable;

    public bool interactionEnabled = true;

    [Header("Text")]
    public TMPro.TextMeshProUGUI interactionText;
    public TMPro.TextMeshProUGUI materialDescriptionText;
    public LayerMask interactableLayer;

    public GameObject interactableButton;
    public Sprite interactableSpriteVis;
    public Sprite interactableSpriteInvis;

    public bool crate;
    public Material outlineMaterial;


    private void Awake()
    {
        if(!instance) instance = this;
    }

    private void Start()
    {
        crate = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer & interactableLayer.value) != 0) && interactionEnabled)
        {
            interactable = (S_Interactable)collider.GetComponent(typeof(S_Interactable));

            if (interactable != null)
            {
                if (collider.CompareTag("Pushable"))
                {
                    crate = true;
                }
                if (collider.CompareTag("Outline"))
                {
                    Material[] tempMaterials = new Material[collider.gameObject.GetComponent<MeshRenderer>().materials.Length];
                    tempMaterials = collider.gameObject.GetComponent<MeshRenderer>().materials;
                    tempMaterials[2] = outlineMaterial;
                    collider.gameObject.GetComponent<MeshRenderer>().materials = tempMaterials;
                }
                interactableButton.GetComponent<Image>().sprite = interactableSpriteVis;
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
        crate = false;
        interactableButton.GetComponent<Image>().sprite = interactableSpriteInvis;
        if (interactable == null) { return; } 
        if (other.gameObject == interactable.gameObject)
        {
            OnInteraction();
        }
        if (other.CompareTag("Outline"))
        {
            Material[] tempMaterials = new Material[other.gameObject.GetComponent<MeshRenderer>().materials.Length];
            tempMaterials = other.gameObject.GetComponent<MeshRenderer>().materials;
            tempMaterials[2] = null;
            other.gameObject.GetComponent<MeshRenderer>().materials = tempMaterials;
        }
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
            case S_Interactable.InteractionType.Hold:
                interactable.Interact();
                break;
        }
    }
    void HandleReleaseInteraction()
    {
        switch (interactable.interactiontype)
        {
            case S_Interactable.InteractionType.Hold:
                interactable.Interact();
                break;
        }
    }
    public void InteractionButton()
    {
        if (interactable)
        {
            HandleInteraction();
        }
    }

    public void ReleaseButton()
    {
        if (interactable)
        {
            HandleReleaseInteraction();
        }
    }
}


