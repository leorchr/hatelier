using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;



public class S_Interactable_Lever : S_Interactable
{
    [Header("Display Text")]
    [SerializeField] private string description = "Press <color=red>RIGHT CLICK</color>";
    [SerializeField] private S_Receiver[] receivers;
    private bool canInteract;

    public override void Interact()
    {
        foreach (S_MovingObject moveObject in receivers) {
            moveObject.Interact();
        }
    }

    public override string GetDescription()
    {
        return description;
    }

    public override string GetMatDescription()
    {
        return "";
    }

    public S_Receiver[] getMoveObjects()
    {
        return receivers;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(S_Interactable_Lever))]
public class Edit_Interactable_Lever : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        var t = target as S_Interactable_Lever;
        var tr = t.transform;
        var pos = tr.position;
        // display an orange disc where the object is
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;
        Handles.DrawWireDisc(pos, tr.up, 1.0f);
        foreach (S_MovingObject moveObject in t.getMoveObjects())
        {
            Handles.DrawDottedLine(tr.position,moveObject.transform.position,5);
        }

        Handles.Slider(tr.position, tr.rotation.eulerAngles);
    }
}
#endif

