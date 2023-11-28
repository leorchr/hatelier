using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Interact_PushPull : S_Interactable
{
    public string description = "Press <color=red>RIGHT CLICK</color>";

    [Header("Booleans")]
    [SerializeField] private bool rightColliderBool;
    [SerializeField] private bool leftColliderBool;
    [SerializeField] private bool frontColliderBool;
    [SerializeField] private bool backColliderBool;
    [Header("GameObject")]
    [SerializeField] private GameObject rightColliderGO;
    [SerializeField] private GameObject leftColliderGO;
    [SerializeField] private GameObject frontColliderGO;
    [SerializeField] private GameObject backColliderGO;
    [SerializeField] private float forceMagnitude;
    [SerializeField] private Rigidbody box;

    public override string GetDescription()
    {
        return description;
    }

    public override string GetMatDescription()
    {
        return "";
    }
    public override void Interact()
    {
        if(rightColliderBool || leftColliderBool || frontColliderBool || backColliderBool )
        {
            Vector3 forceDirection = box.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            box.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
        else if (!rightColliderBool || !leftColliderBool || !frontColliderBool || !backColliderBool)
        {
            forceMagnitude = 0;
        }
    }

    private void Start()
    {
        if (rightColliderBool)
        {
            rightColliderGO.SetActive(true);
        }
        else if(!rightColliderBool) 
        {
            rightColliderGO.SetActive(false);
        }

        if (leftColliderBool) 
        {
            leftColliderGO.SetActive(true);
        }
        else if (!leftColliderBool)
        {
            leftColliderGO.SetActive(false);
        }

        if (frontColliderBool)
        {
            frontColliderGO.SetActive(true);
        }
        else if (!frontColliderBool)
        {
            frontColliderGO.SetActive(false);
        }

        if(backColliderBool)
        {
            backColliderGO.SetActive(true);
        }
        else if (!backColliderBool)
        {
            backColliderGO.SetActive(false);
        }

        else
        {
            return;
        }
    }
}
