using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class S_Pressure_Plate : MonoBehaviour
{
    public enum PressureType
    {
        Toggle,
        Hold
    }

    public PressureType type = PressureType.Hold;

    [SerializeField] private S_MovingObject[] moveObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            switch (type)
            {
                case PressureType.Toggle:
                    Activate();
                    break;
                case PressureType.Hold:
                    Activate();
                    break;
                default:

                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case PressureType.Hold:
                    Activate();
                    break;
                default:

                    break;
            }
        }
    }

    public void Activate()
    {
        foreach (S_MovingObject moveObject in moveObjects)
        {
            moveObject.Interact();
        }
    }

    public S_MovingObject[] getMoveObjects() { return  moveObjects; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(S_Pressure_Plate))]
public class Edit_Plate : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        var t = target as S_Pressure_Plate;
        var tr = t.transform;
        var pos = tr.position;
        // display an orange disc where the object is
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;
        Handles.DrawWireDisc(pos, tr.up, 1.0f);
        foreach (S_MovingObject moveObject in t.getMoveObjects())
        {
            Handles.DrawDottedLine(tr.position, moveObject.transform.position, 5);
        }

        Handles.Slider(tr.position, tr.rotation.eulerAngles);
    }
}
#endif