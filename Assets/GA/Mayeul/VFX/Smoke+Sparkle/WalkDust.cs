using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


[ExecuteInEditMode]
public class WalkDust : MonoBehaviour
{
    public bool VFXTrigger;
    [SerializeField] private VisualEffect visualEffect;

    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            visualEffect.Play();
        }
    }
}
