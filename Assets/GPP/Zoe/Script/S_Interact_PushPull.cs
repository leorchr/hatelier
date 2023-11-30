using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum dir
{
    Left,
    Right,
    Front,
    Back,
    none
}

public class S_Interact_PushPull : S_Interactable
{
    public string description = "Press <color=red>RIGHT CLICK</color>";

    Rigidbody rb;

    dir currentDir = dir.none;

    [Header("Booleans")]
    [SerializeField] private bool rightColliderBool;
    [SerializeField] private bool leftColliderBool;
    [SerializeField] private bool frontColliderBool;
    [SerializeField] private bool backColliderBool;
    [Header("GameObject")]
    [SerializeField] private Transform rightColliderGO;
    [SerializeField] private Transform leftColliderGO;
    [SerializeField] private Transform frontColliderGO;
    [SerializeField] private Transform backColliderGO;
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
        if (!S_PlayerController.instance.m_isPushing) {
            if (rightColliderBool || leftColliderBool || frontColliderBool || backColliderBool)
            {
                dir d = getSideCloser();
                S_PlayerController.instance.setDir(d);
                S_PlayerController.instance.m_isPushing = true;
                S_PlayerController.instance.m_PushedObject = gameObject;
                switch (d)
                {
                    case dir.Left:
                        S_PlayerController.instance.transform.position = leftColliderGO.transform.position;
                        break;
                    case dir.Right:
                        S_PlayerController.instance.transform.position = rightColliderGO.transform.position;
                        break;
                    case dir.Front:
                        S_PlayerController.instance.transform.position = frontColliderGO.transform.position;
                        break;
                    case dir.Back:
                        S_PlayerController.instance.transform.position = backColliderGO.transform.position;
                        break;
                    default: break;

                }
                transform.parent = S_PlayerController.instance.transform;
            }
        }
        else if (S_PlayerController.instance.m_PushedObject == gameObject)
        {
            S_PlayerController.instance.setDir(dir.none);
            S_PlayerController.instance.m_PushedObject = null;
            S_PlayerController.instance.m_isPushing = false;

            transform.parent = null;
        }
        
    }

    private dir getSideCloser()
    {
        Vector3 playerPos = S_PlayerController.instance.transform.position;
        float minDist = Mathf.Infinity;
        Transform closest = null;
        foreach( Transform t in transform )
        {
            float dist = Vector3.Distance(t.position, playerPos);
            if ( dist < minDist )
            {
                closest = t;
                minDist = dist;
            }
        }

        if (closest == rightColliderGO) { return dir.Right; }
        if (closest == leftColliderGO) { return dir.Left; }
        if (closest == backColliderGO) { return dir.Back; }
        if (closest == frontColliderGO) { return dir.Front; }
        return dir.none;
    }

    private void Start()
    {
        rightColliderGO.gameObject.SetActive(rightColliderBool);
        leftColliderGO.gameObject.SetActive(leftColliderBool);
        backColliderGO.gameObject.SetActive(backColliderBool);
        frontColliderGO.gameObject.SetActive(frontColliderBool);

        rb= GetComponent<Rigidbody>();
    }
}
