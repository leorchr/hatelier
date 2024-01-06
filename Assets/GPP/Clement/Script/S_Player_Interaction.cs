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

    public bool crate = false;
    public bool lockInteract = false;
    public Material outlineMaterial;

    public List<S_Interactable> interactableList;


    private void Awake()
    {
        if(!instance) instance = this;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer & interactableLayer.value) != 0) && interactionEnabled)
        {
            S_Interactable interactableT = (S_Interactable)collider.GetComponent(typeof(S_Interactable));

            if (interactableT != null)
            {
                interactableList.Add(interactableT);
                checkInteract();
            }
        }
    }

    private void Update()
    {
        Debug.Log(crate);
        if (interactableList.Count > 1)
        {
            checkInteract(); 
        }
        
    }

    public void checkInteract()
    {
        if (!lockInteract)
        {
            foreach (S_Interactable go in interactableList)
            {
                if (go != interactable)
                {
                    //Check if other interactable are closer than the current one
                    if ( interactable == null || (Vector3.Distance(transform.position, interactable.transform.position) > Vector3.Distance(transform.position, go.transform.position)))
                    {
                        //Remove highlight from old interactable
                        if (interactable != null)
                        {
                            if (interactable.CompareTag("Outline")) //Remove outline
                            {
                                Material[] tempMaterials = new Material[interactable.gameObject.GetComponent<MeshRenderer>().materials.Length];
                                tempMaterials = interactable.gameObject.GetComponent<MeshRenderer>().materials;
                                tempMaterials[2] = null;
                                interactable.gameObject.GetComponent<MeshRenderer>().materials = tempMaterials;
                            }
                        }
                        //Change to new interactable
                        interactable = go;
                        if (interactable.CompareTag("Outline")) //Set the outline
                        {
                            Material[] tempMaterials = new Material[interactable.gameObject.GetComponent<MeshRenderer>().materials.Length];
                            tempMaterials = interactable.gameObject.GetComponent<MeshRenderer>().materials;
                            tempMaterials[2] = outlineMaterial;
                            interactable.gameObject.GetComponent<MeshRenderer>().materials = tempMaterials;
                        }
                        interactableButton.GetComponent<Image>().sprite = interactableSpriteVis;
                        interactionText.text = interactable.GetDescription();
                        interactionText.gameObject.SetActive(true);

                        materialDescriptionText.text = interactable.GetMatDescription();
                        materialDescriptionText.gameObject.SetActive(true);

                        
                    }
                }
                else if (go == null){

                    while (interactableList.Contains(go)) //Remove all ref of this object in the list
                    {
                        interactableList.Remove(go);
                    }

                    if (interactableList.Count == 0)
                    {
                            OnInteraction();
                    }
                }
            }
            
        }
        crate = interactable.CompareTag("Pushable");
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (interactableList.Count == 0) { return; }
        S_Interactable interactableT = other.GetComponent<S_Interactable>();
        if (interactableT != null)
        {
            if (interactableT.CompareTag("Outline")) //Remove outline
            {
                Material[] tempMaterials = new Material[interactableT.gameObject.GetComponent<MeshRenderer>().materials.Length];
                tempMaterials = interactableT.gameObject.GetComponent<MeshRenderer>().materials;
                tempMaterials[2] = null;
                interactableT.gameObject.GetComponent<MeshRenderer>().materials = tempMaterials;
            }
            while (interactableList.Contains(other.gameObject.GetComponent<S_Interactable>())) //Remove all ref of this object in the list
            {
                interactableList.Remove(other.gameObject.GetComponent<S_Interactable>());
            }
            
            switch (interactableList.Count)
            {
                case 0:
                    OnInteraction();
                    break;
                case 1:
                    checkInteract();
                    break;
                default:
                    break;
            }
        }
    }

    public void OnInteraction()
    {
        interactable = null;
        interactionText.gameObject.SetActive(false);
        materialDescriptionText.gameObject.SetActive(false);
        interactableButton.GetComponent<Image>().sprite = interactableSpriteInvis;
        crate = false;
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


