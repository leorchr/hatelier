using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class S_CameraTransition : MonoBehaviour
{
    [Space]
    [Header("Positions \n-------------------------")]
    [Space]
    [SerializeField] private S_CameraRail camPos1;
    [SerializeField] private S_CameraRail camPos2;
    private S_CameraRail currentCamRail;
    [SerializeField] private Transform playerPos1;
    [SerializeField] private Transform playerPos2;

    [Space]
    [Header("Smooth Damp \n-------------------------")]
    [Space]
    [SerializeField] private float smoothDampAccuracy = 0.2f;
    [SerializeField] private float movementSmoothTime = 0.2f;
    private Vector3 targetPosition;
    private Vector3 moveVelocity;

    private bool canMove = false;
    private static bool movementOngoing = false;

    [Space]
    [TextArea]
    public string notes = " - Tag Player sur le joueur\n - Tag Main Camera sur la caméra\n - Le collider doit être trigger\n - Doit avoir 2 références de camera rails au minimum";

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (movementOngoing) return;

        /*if (other.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(other.transform.position, playerPos1.position) < Vector3.Distance(other.transform.position, playerPos2.position))   // Destination selection
            {
                currentCamRail = camPos2;
                other.transform.position = playerPos2.position;
            }
            else
            {
                currentCamRail = camPos1;
                other.transform.position = playerPos1.position;
            }
            S_CameraMovements.instance.SetupCameraMovements(currentCamRail);
        }*/

        if (other.gameObject.CompareTag("Player"))
        {
            S_CameraMovements.instance.StopCameraMovements();

            if(Vector3.Distance(other.transform.position, playerPos1.position) < Vector3.Distance(other.transform.position, playerPos2.position))   // Destination selection
            {
                currentCamRail = camPos2;
                targetPosition = new Vector3(0, camPos2.start.position.y, camPos2.start.position.z);
                targetPosition.x = Mathf.Clamp(playerPos2.transform.position.x, camPos2.start.position.x, camPos2.end.position.x);

                // Define target position for horizontal and vertical
                /*if (camPos2.switchAxis)
                {
                    targetPosition = new Vector3(0, camPos2.start.position.y, camPos2.start.position.z);
                    targetPosition.x = Mathf.Clamp(playerPos2.transform.position.x, camPos2.start.position.x, camPos2.end.position.x);
                }
                else
                {
                    targetPosition = new Vector3(camPos2.start.position.x, camPos2.start.position.y, 0);
                    targetPosition.z = Mathf.Clamp(playerPos2.transform.position.z, camPos2.start.position.z, camPos2.end.position.z);
                }*/

                other.transform.position = playerPos2.position;
            }
            else
            {
                currentCamRail = camPos1;
                targetPosition = new Vector3(0, camPos1.start.position.y, camPos1.start.position.z);
                targetPosition.x = Mathf.Clamp(playerPos1.transform.position.x, camPos1.start.position.x, camPos1.end.position.x);
                
                /*if (camPos1.switchAxis)
                {
                    targetPosition = new Vector3(0, camPos1.start.position.y, camPos1.start.position.z);
                    targetPosition.x = Mathf.Clamp(playerPos1.transform.position.x, camPos1.start.position.x, camPos1.end.position.x);
                }
                else
                {
                    targetPosition = new Vector3(camPos1.start.position.x, camPos1.start.position.y, 0);
                    targetPosition.z = Mathf.Clamp(playerPos1.transform.position.z, camPos1.start.position.z, camPos1.end.position.z);
                }*/
                
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
            if (Vector3.Distance(Camera.main.transform.position, targetPosition) <= smoothDampAccuracy) {
                canMove = false;
                movementOngoing = false;
                S_CameraMovements.instance.SetupCameraMovements(currentCamRail);
            }
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
