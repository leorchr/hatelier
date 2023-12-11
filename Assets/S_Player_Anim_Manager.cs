using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class S_Player_Anim_Manager : MonoBehaviour
{
    private Animator c_Animator;

    public VisualEffect DustVFX;

    // Start is called before the first frame update
    void Start()
    {
        c_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walkDust()
    {
        Vector3 footPos = Vector3.zero;
        
        DustVFX.SetVector3("Foot_Location", footPos);
        DustVFX.Play();

        S_PlayerController.instance.StepSound();
    }
}
