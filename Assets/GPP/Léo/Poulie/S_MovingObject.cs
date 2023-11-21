using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_MovingObject : MonoBehaviour
{
    public enum PoulieState
    {
        Down,
        Moving,
        Up
    }

    [SerializeField] private float distanceMovement;
    public PoulieState positionState = PoulieState.Down;


    [Header("Smooth Damp")]
    [SerializeField] private float smoothDampAccuracy = 0.2f;
    [SerializeField] private float movementSmoothTime = 0.2f;
    private Vector3 targetPosition;
    private Vector3 moveVelocity;
    [HideInInspector] public Vector3 upPos, downPos;


    private void Start()
    {
        if(positionState == PoulieState.Down)
        {
            Vector3 up = new Vector3(0, distanceMovement, 0);
            upPos = transform.position + up;
            downPos = transform.position;
        }
        if (positionState == PoulieState.Up)
        {
            Vector3 down = new Vector3(0, distanceMovement, 0);
            downPos = transform.position - down;
            upPos = transform.position;
        }
    }

    private void Update()
    {
        if (positionState == PoulieState.Moving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, movementSmoothTime);

            float dist = Vector3.Distance(transform.position, targetPosition);

            if (dist <= smoothDampAccuracy && targetPosition == upPos) positionState = PoulieState.Up;
            if (dist <= smoothDampAccuracy && targetPosition == downPos) positionState = PoulieState.Down;
        }
    }

    public void Interact()
    {
        if (positionState == PoulieState.Down)
        {
            positionState = PoulieState.Moving;
            targetPosition = upPos;
        }
        else if (positionState == PoulieState.Up)
        {
            positionState = PoulieState.Moving;
            targetPosition = downPos;
        }
    }
}