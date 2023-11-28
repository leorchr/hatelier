using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class S_MovingObject : MonoBehaviour
{
    public enum MoveType
    {
        SmoothDamp,
        Curve
    }
    [HideInInspector]
    public MoveType moveType = MoveType.SmoothDamp;


    [SerializeField] private Transform posBegin;
    [SerializeField] private Transform posEnd;

    [HideInInspector]
    public float smoothDampAccuracy = 0.2f;
    [HideInInspector]
    public float movementSmoothTime = 0.2f;

    [HideInInspector] public AnimationCurve moveCurve = AnimationCurve.EaseInOut(0,0,1,1);

    private Vector3 targetPosition;
    private Vector3 moveVelocity;


    private void Start()
    {
        
    }

    private void Update()
    {

        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, movementSmoothTime);
    }

    public void Interact()
    {
        
    }

    public void CreatePos()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        GameObject gB = new GameObject();
        gB.name = "Begin Position";
        gB.transform.parent = transform;
        gB.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        var iconContent = EditorGUIUtility.IconContent("sv_label_1");
        EditorGUIUtility.SetIconForObject(gB, (Texture2D)iconContent.image);
        posBegin = gB.transform;
        
        GameObject gE = new GameObject();
        gE.name = "End Position";
        gE.transform.parent = transform;
        gE.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        var iconContent2 = EditorGUIUtility.IconContent("sv_label_2");
        EditorGUIUtility.SetIconForObject(gE, (Texture2D)iconContent2.image);
        posEnd = gE.transform;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(S_MovingObject))]
public class Edit_Moving_Object : Editor
{

    SerializedProperty m_MoveType;
    SerializedProperty m_SmoothDampAccuracy;
    SerializedProperty m_MovementSmoothTime;
    SerializedProperty m_MoveCurve;

    void OnEnable()
    {
        // Fetch the objects from the GameObject script to display in the inspector
        m_MoveType = serializedObject.FindProperty("moveType");
        m_SmoothDampAccuracy = serializedObject.FindProperty("smoothDampAccuracy");
        m_MovementSmoothTime = serializedObject.FindProperty("movementSmoothTime");

        m_MoveCurve = serializedObject.FindProperty("moveCurve");
    }
    public override void OnInspectorGUI()
    {
        var t = target as S_MovingObject;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Movement Customisation", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_MoveType);

        switch (m_MoveType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(m_SmoothDampAccuracy);
                EditorGUILayout.PropertyField(m_MovementSmoothTime);
                break;
            case 1:
                EditorGUILayout.PropertyField(m_MoveCurve);
                break;
            default:

                break;
        }

        if (GUILayout.Button("Create Pos"))
        {
            t.CreatePos();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif