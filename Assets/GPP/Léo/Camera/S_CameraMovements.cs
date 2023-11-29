using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DeadZoneStatus
{
    In, Out, CatchingUp
}

public class S_CameraMovements : MonoBehaviour
{
    public static S_CameraMovements instance;

    [HideInInspector] public bool canMove = false;
    [HideInInspector] public Vector3 offset;
    private Vector3 targetPosition;
    [SerializeField] private S_CameraRail currentRail;

    [Space]
    [Header("Smooth Damp \n-------------------------")]
    [Space]
    [Min(0)][SerializeField] private float movementSmoothTime = 0.2f;
    private Vector3 moveVelocity;

    [Space]
    [Header("Dead Zone\n-------------------------")]
    [Space]
    [Tooltip("Deadzone size must be higher than target zone size")]
    [Min(0)][Range(0.0f, 1.0f)][SerializeField] private float deadZoneSize = 2f;
    [Min(0)][Range(0.0f,1.0f)][SerializeField] private float targetZoneSize = 0.1f;
    [SerializeField] private DeadZoneStatus deadZoneStatus = DeadZoneStatus.In;

    [Space]
    [Header("Debugging \n-------------------------")]
    [Space]
    [Tooltip("Enable visual debugging")]
    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color deadZoneColor= new Color(0.75f, 0.2f, 0.2f, 1f);
    [SerializeField] private Color targetZoneColor= new Color(0f, 1f, 0.2f, 1f);

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        transform.position = new Vector3(S_PlayerController.instance.transform.position.x, currentRail.start.position.y, currentRail.start.position.z);
        offset = transform.position - S_PlayerController.instance.transform.position;
        canMove = true;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (canMove)
        {
            Deadzone();
            targetPosition.x = Mathf.Clamp(targetPosition.x + currentRail.offset, currentRail.start.position.x, currentRail.end.position.x);
            Camera.main.transform.position = new Vector3(Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref moveVelocity, movementSmoothTime).x, Camera.main.transform.position.y, Camera.main.transform.position.z);

            /*if (currentRail.switchAxis)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x + currentRail.offset, currentRail.start.position.x, currentRail.end.position.x);
                Camera.main.transform.position = new Vector3(Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref moveVelocity, movementSmoothTime).x, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
            else
            {
                targetPosition.z = Mathf.Clamp(targetPosition.z + currentRail.offset, currentRail.start.position.z, currentRail.end.position.z);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref moveVelocity, movementSmoothTime).z);
            }*/
        }
    }

    private void Deadzone()
    {
        Vector3 playerPos = S_PlayerController.instance.transform.position;
        float distanceToTarget = System.Math.Abs((playerPos + offset).x - transform.position.x);


        /*if (currentRail.switchAxis)      // Horizontal Camera
        {
            distanceToTarget = System.Math.Abs((playerPos + offset).x - transform.position.x);
        }
        else                // Vertical Camera
        {
            distanceToTarget = System.Math.Abs((playerPos + offset).z - transform.position.z);
        }*/


        if (distanceToTarget > deadZoneSize && deadZoneStatus == DeadZoneStatus.In)
        {
            deadZoneStatus = DeadZoneStatus.Out;
            targetPosition = playerPos + offset;
        }
        else
        {
            switch (deadZoneStatus)
            {
                case DeadZoneStatus.In:
                    targetPosition = transform.position;
                    break;
                case DeadZoneStatus.Out:
                    targetPosition = playerPos + offset;
                    deadZoneStatus = DeadZoneStatus.CatchingUp;
                    break;
                case DeadZoneStatus.CatchingUp:
                    targetPosition = playerPos + offset;
                    if (distanceToTarget < targetZoneSize)
                    {
                        deadZoneStatus = DeadZoneStatus.In;
                    }
                    break;
            }
        }
    }

    public void StopCameraMovements()
    {
        S_CameraMovements.instance.canMove = false;
    }

    public void SetupCameraMovements(S_CameraRail newRail)
    {
        currentRail = newRail;
        offset = new Vector3(0, transform.position.y - S_PlayerController.instance.transform.position.y, transform.position.z - S_PlayerController.instance.transform.position.z);
        Debug.Log(offset.x);

        /*if (currentRail.switchAxis)
        {
            //offset = new Vector3(0, currentRail.start.position.y - S_PlayerController.instance.transform.position.y, currentRail.start.position.z - S_PlayerController.instance.transform.position.z);
            offset = new Vector3(0, transform.position.y - S_PlayerController.instance.transform.position.y, transform.position.z - S_PlayerController.instance.transform.position.z);

            //Debug.Log(offset);
        }
        else
        {
            //offset = new Vector3(currentRail.start.position.x - S_PlayerController.instance.transform.position.x, currentRail.start.position.y - S_PlayerController.instance.transform.position.y, 0);
            offset = new Vector3(transform.position.x - S_PlayerController.instance.transform.position.x, transform.position.y - S_PlayerController.instance.transform.position.y, 0);
        }*/

        canMove = true;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (!visualDebugging)
            return;

        Handles.color = deadZoneColor;
        Handles.RadiusHandle(Quaternion.identity, transform.position - offset, deadZoneSize);
        Handles.color = targetZoneColor;
        Handles.RadiusHandle(Quaternion.identity, transform.position - offset, targetZoneSize);
    }

#endif

}
