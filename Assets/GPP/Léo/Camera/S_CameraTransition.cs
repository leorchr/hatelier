using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class S_CameraTransition : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Transform camPos1;
    [SerializeField] private Transform camPos2;
    [SerializeField] private Transform playerPos1;
    [SerializeField] private Transform playerPos2;

    [Header("Smooth Damp")]
    [SerializeField] private float smoothDampAccuracy = 0.2f;
    [SerializeField] private float movementSmoothTime = 0.2f;
    private Vector3 targetPosition;
    private Vector3 moveVelocity;

    private bool canMove = false;
    private static bool movementOngoing = false;

    [Space]
    [TextArea]
    public string notes = " - Tag Player sur le joueur\n - Tag Main Camera sur la caméra\n - Le collider doit être trigger";

    [Space]
    [Header("Debugging \n-------------------------")]
    [Space]

    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color positionColor = new Color(0.75f, 0.2f, 0.2f, 0.75f);
    [Range(0f,10f)] [SerializeField] private float wireRadius = 1f;


    private void Start()
    {
        canMove = false;
        movementOngoing = false;
        targetPosition = camPos1.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (movementOngoing) return;
        if (other.gameObject.CompareTag("Player"))
        {
            if(Vector3.Distance(other.transform.position, playerPos1.position) < Vector3.Distance(other.transform.position, playerPos2.position))
            {
                targetPosition = camPos2.position;
                other.transform.position = playerPos2.position;
            }
            else
            {
                targetPosition = camPos1.position;
                other.transform.position = playerPos1.position;
            }
            canMove = true;
            movementOngoing = true;
        }
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (canMove)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref moveVelocity, movementSmoothTime);
            if (Vector3.Distance(Camera.main.transform.position, targetPosition) <= smoothDampAccuracy) { canMove = false; movementOngoing = false; }
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (!visualDebugging)
            return;

        Handles.color = positionColor;
        Handles.DrawWireDisc(playerPos1.transform.position, playerPos1.transform.up, wireRadius);
        Handles.DrawWireDisc(playerPos2.transform.position, playerPos2.transform.up, wireRadius);
    }

#endif

}
