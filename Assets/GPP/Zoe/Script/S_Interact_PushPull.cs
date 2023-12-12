using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
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

    public bool isPushed = false;

    public string description = "Press <color=red>RIGHT CLICK</color>";

    [Space(5)]
    public Side[] sides;

    private BoxCollider bc;

    [Header("Arrow")]
    public GameObject verticalArrows;
    public GameObject horizontalArrows;

    public GameObject crate;

    private void Start()
    {
        foreach (Side s in sides)
        {
            s.transform.gameObject.SetActive(s.active);
        }
        bc = transform.parent.GetComponent<BoxCollider>();
        horizontalArrows.SetActive(false);
        verticalArrows.SetActive(false);
    }

    private void Update()
    {
        if (S_Player_Interaction.instance.crate == true)
        {
            crate.GetComponent<Animator>().SetBool("Bool_Material", true);
        }
        else if (S_Player_Interaction.instance.crate == false)
        {
            crate.GetComponent<Animator>().SetBool("Bool_Material", false);
        }
    }

    public override string GetDescription()
    {
        S_Player_Interaction.instance.crate = true;
        return description;
    }

    public override string GetMatDescription()
    {
        return "";
    }
    public override void Interact()
    {
        S_Player_Interaction.instance.crate = false;

        if (!S_PlayerController.instance.m_isPushing) 
        {
            if (hasAtLeastOneSideActive() && getSideCloser().active)
            {
                Side s = getSideCloser();
                if(s.side == dir.Left  || s.side == dir.Right)
                {
                    horizontalArrows.SetActive(true);
                }
                else if(s.side == dir.Front || s.side == dir.Back)
                {
                    verticalArrows.SetActive(true);
                }

                S_PlayerController.instance.setDir(s.side);
                S_PlayerController.instance.m_isPushing = true;
                S_PlayerController.instance.m_PushedObject = transform.parent.gameObject;
                S_PlayerController.instance.transform.position = s.transform.position;

                S_Player_Interaction.instance.interactionEnabled = false;

                bc.enabled = false;

                S_PlayerController.instance.createCollider(true, s);

                S_PlayerController.instance.transform.rotation = s.transform.rotation;

                transform.parent.parent = S_PlayerController.instance.transform;

                isPushed = true;

            }
        }

        else if (S_PlayerController.instance.m_PushedObject == transform.parent.gameObject)
        {
            horizontalArrows.SetActive(false); 
            verticalArrows.SetActive(false);
            S_Player_Interaction.instance.interactionEnabled = true;
            S_PlayerController.instance.setDir(dir.none);
            S_PlayerController.instance.m_PushedObject = null;
            S_PlayerController.instance.m_isPushing = false;

            S_PlayerController.instance.createCollider(false,new Side());

            isPushed = false;

            transform.parent.parent = null;

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

   
}
