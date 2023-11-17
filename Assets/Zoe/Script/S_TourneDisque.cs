using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TourneDisque : MonoBehaviour
{
    //si true = gauche // si false = droite
    public bool turnToLeft;
    public float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushToTurn()
    {
        if (turnToLeft) 
        {
            transform.Rotate(new Vector3(0, -rotationAngle, 0));
        }
        else if (!turnToLeft)
        {
            transform.Rotate(new Vector3(0,rotationAngle, 0));
        }
    }

}
