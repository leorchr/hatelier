using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class s_Sync_Moving : MonoBehaviour
{
    //Serializable variable
    [HideInInspector] public MoveType moveType = MoveType.SmoothDamp;
    [HideInInspector] public float moveAccuracy = 0.2f, movementSmoothTime = 0.2f;
    [HideInInspector] public AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [HideInInspector] public float curveTime = 1.0f, debugCurvePrevis = 10;
    [HideInInspector] public Color debugColor = Color.red;
    [HideInInspector] public bool isAutomatic = false;
    [HideInInspector] public float timeBetween = 0;

    private float movingFinishedN = 0;

    S_MovingObject[] s_MovingObjects;
    // Start is called before the first frame update

    void Start()
    {
        
        int i = 0;
        foreach (Transform t in transform)
        {
            i++;
        }
        s_MovingObjects = new S_MovingObject[i];
        i = 0;
        foreach (Transform t in transform)
        {
            if (t.GetComponent<S_MovingObject>() != null)
            {
                s_MovingObjects[i] = t.GetComponent<S_MovingObject>();
                i++;
            }
        }
        ChangeDirAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addOneFinished()
    {
        movingFinishedN++;

        if (movingFinishedN == s_MovingObjects.Length)
        {
            movingFinishedN = 0;

            Invoke("ChangeDirAll", timeBetween);
        }
    }

    public void stopAll()
    {
        foreach (S_MovingObject s in s_MovingObjects)
        {
            s.AntiUnsync = true;
        }
    }

    public void moveAll()
    {
        foreach (S_MovingObject s in s_MovingObjects)
        {
            s.AntiUnsync = false;
        }
    }

    private void ChangeDirAll()
    {
        foreach (S_MovingObject s in s_MovingObjects)
        {
            s.Interact();
        }
    }

#if UNITY_EDITOR
    public void ChangeVarInOtherObject()
    {
        foreach (Transform t in transform)
        {
            if (t.GetComponent<S_MovingObject>() != null){
                S_MovingObject mo = t.GetComponent<S_MovingObject>();

                mo.isAutomatic = false;
                mo.timeBetween = 0;

                mo.moveType = moveType;
                mo.moveAccuracy = moveAccuracy;
                mo.movementSmoothTime = movementSmoothTime;
                mo.moveCurve = moveCurve;
                mo.curveTime = curveTime;

                EditorUtility.SetDirty(mo);
            }
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(s_Sync_Moving))]
public class Edit_Sync_Moving : Editor
{
    SerializedProperty m_IsAutomatic;
    SerializedProperty m_TimeBetween;
    SerializedProperty m_CurveTime;
    SerializedProperty m_MoveType;
    SerializedProperty m_MoveAccuracy;
    SerializedProperty m_MovementSmoothTime;
    SerializedProperty m_MoveCurve;

    //Debug Variables
    SerializedProperty m_DebugColor;
    SerializedProperty m_DebugCurvePrevis;

    void OnEnable()
    {
        var t = target as s_Sync_Moving;
        // Fetch the objects from the GameObject script to display in the inspector
        m_IsAutomatic = serializedObject.FindProperty("isAutomatic");
        m_TimeBetween = serializedObject.FindProperty("timeBetween");

        m_MoveType = serializedObject.FindProperty("moveType");
        m_MoveAccuracy = serializedObject.FindProperty("moveAccuracy");
        m_MovementSmoothTime = serializedObject.FindProperty("movementSmoothTime");

        m_MoveCurve = serializedObject.FindProperty("moveCurve");
        m_CurveTime = serializedObject.FindProperty("curveTime");
    }


    public override void OnInspectorGUI()
    {
        var t = target as s_Sync_Moving;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(m_IsAutomatic);
        if (m_IsAutomatic.boolValue)
        {
            EditorGUILayout.PropertyField(m_TimeBetween);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Movement Customisation", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(m_MoveType);
        EditorGUILayout.PropertyField(m_MoveAccuracy);
        switch (m_MoveType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(m_MovementSmoothTime);
                break;
            case 1:
                EditorGUILayout.PropertyField(m_MoveCurve);
                EditorGUILayout.PropertyField(m_CurveTime);
                break;
            default:

                break;
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(t);
            t.ChangeVarInOtherObject();

        }
            
    }

}
#endif
