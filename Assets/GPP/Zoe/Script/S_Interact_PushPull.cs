using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dir
{
    Left,
    Right,
    Front,
    Back,
    none
}

[Serializable]
public class Side
{
    public Transform transform;
    public dir side;
    public bool active;
}


public class S_Interact_PushPull : S_Interactable
{
    
    

    public string description = "Press <color=red>RIGHT CLICK</color>";

    Rigidbody rb;

    dir currentDir = dir.none;

    [Space(5)]
    public Side[] sides;

    private BoxCollider bc;

    

    
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
            if (hasAtLeastOneSideActive() && getSideCloser().active)
            {
                Side s = getSideCloser();
                S_PlayerController.instance.setDir(s.side);
                S_PlayerController.instance.m_isPushing = true;
                S_PlayerController.instance.m_PushedObject = gameObject;
                S_PlayerController.instance.transform.position = s.transform.position;

                S_Player_Interaction.instance.interactionEnabled = false;

                bc.enabled = false;

                Quaternion oldRotation = S_PlayerController.instance.transform.rotation;

                S_PlayerController.instance.createCollider(true, s);

                S_PlayerController.instance.transform.rotation = s.transform.rotation;

                transform.parent = S_PlayerController.instance.transform;

                

            }
        }
        else if (S_PlayerController.instance.m_PushedObject == gameObject)
        {
            S_Player_Interaction.instance.interactionEnabled = true;
            S_PlayerController.instance.setDir(dir.none);
            S_PlayerController.instance.m_PushedObject = null;
            S_PlayerController.instance.m_isPushing = false;

            S_PlayerController.instance.createCollider(false,new Side());

            

            transform.parent = null;

            bc.enabled = true;
        }
        
    }

    private bool hasAtLeastOneSideActive()
    {
        bool result = false;
        foreach(Side s in sides)
        {
            if (s.active) { result = true; break; }
        }
        return result;
    }

    private Side getSideCloser()
    {
        Vector3 playerPos = S_PlayerController.instance.transform.position;
        float minDist = Mathf.Infinity;
        Side closest = new Side();
        foreach( Side s in sides )
        {
            Transform t = s.transform;
            float dist = Vector3.Distance(t.position, playerPos);
            if ( dist < minDist )
            {
                closest = s;
                minDist = dist;
            }
        }
        return closest;
    }

    private void Start()
    {
       foreach(Side s in sides)
        {
            s.transform.gameObject.SetActive(s.active);
        }

        rb= GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }
}
