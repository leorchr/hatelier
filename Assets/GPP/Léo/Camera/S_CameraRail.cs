using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class S_CameraRail : MonoBehaviour
{
    public Transform start, end;
    //[Tooltip("Switch between horizontal and vertical axis. False for vertical axis. True for horizontal axis")]
    //public bool switchAxis;
    [Tooltip("Camera offset. X or Z depending rail axis")]
    public float offset;  // ne marche pas

#if UNITY_EDITOR
    [Space]
    [Header("Debugging \n-------------------------")]
    [Space]
    [Tooltip("Enable visual debugging")]
    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color railColor = new Color(0.0f, 0.0f, 1f, 1f);
    [Min(0)][Range(0.0f, 10.0f)][SerializeField] private float width = 3f;
#endif


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (!visualDebugging)
            return;

        Handles.color = railColor;
        Handles.DrawAAPolyLine(width, start.position, end.position);
    }

#endif
}
