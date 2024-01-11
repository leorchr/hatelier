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

    public bool playerCanActivate = true, crateCanActivate = true;

    public List<GameObject> onPlate = new List<GameObject>();

    public PressureType type = PressureType.Hold;

    Animator anim;

    [SerializeField] private S_Receiver[] receivers;
    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((other.gameObject.CompareTag("Player") && playerCanActivate) || (other.gameObject.CompareTag("Pushable") && crateCanActivate)))
        {
            bool canActivate = true;
            if (other.gameObject.CompareTag("Pushable"))
            {
                canActivate = other.GetComponentInChildren<S_Interact_PushPull>().isPushed;
            }
            if (canActivate)
            {
                S_SoundManager.instance.PlaySound(soundType.Interaction_Plate);
                onPlate.Add(other.gameObject);
                if (onPlate.Count == 1)
                {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((other.gameObject.CompareTag("Player") && playerCanActivate) || (other.gameObject.CompareTag("Pushable") && crateCanActivate)))
        {
            while (onPlate.Contains(other.gameObject))
            {
                onPlate.Remove(other.gameObject);
            }
            if (onPlate.Count == 0)
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
    }

    public void Activate()
    {
        door.GetComponent<Animator>().SetBool("GetStatue", onPlate.Count != 0);
        S_SoundManager.instance.PlaySound(soundType.Door_Open);
        anim.SetBool("isPressed", onPlate.Count != 0);

        //foreach (S_Receiver moveObject in receivers)
        //{
        //    moveObject.Interact();
        //}
    }

    public S_Receiver[] getMoveObjects() { return  receivers; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(S_Pressure_Plate))]
public class Edit_Plate : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    //public void OnSceneGUI()
    //{
    //    var t = target as S_Pressure_Plate;
    //    var tr = t.transform;
    //    var pos = tr.position;
    //    // display an orange disc where the object is
    //    var color = new Color(1, 0.8f, 0.4f, 1);
    //    Handles.color = color;
    //    Handles.DrawWireDisc(pos, tr.up, 1.0f);
    //    foreach (S_MovingObject moveObject in t.getMoveObjects())
    //    {
    //        if (moveObject != null)
    //        {
    //            Handles.DrawDottedLine(tr.position, moveObject.transform.position, 5);
    //        }
    //    }
    //}
}
#endif