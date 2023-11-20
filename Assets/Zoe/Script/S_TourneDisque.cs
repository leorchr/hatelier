using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class S_TourneDisque : MonoBehaviour
{
    //si true = gauche // si false = droite
    public bool turnToLeft;
    public float speedRotation;
    public float rotationAngle;
    
    public bool isPressed;
    private Vector3 rotation;
    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        rotation = new Vector3(0, rotationAngle, 0);
        rot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(turnToLeft)
        {
            rot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, speedRotation * Time.deltaTime);
        }
        else
        {
            rot = Quaternion.Euler(rotation.x, 0, rotation.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, speedRotation * Time.deltaTime);
        }
    }



    public void PushToTurn()
    {
        if (turnToLeft) 
        {
            turnToLeft = false;
        }
        else
        {
            turnToLeft = true;
        }        
    }
}
