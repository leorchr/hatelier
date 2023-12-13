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
    public void InteractionButton()
    {
        if (interactable)
        {
            HandleInteraction();
        }
    }
}


