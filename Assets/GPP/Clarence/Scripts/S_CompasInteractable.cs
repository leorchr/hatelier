using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class S_CompasInteractable : S_Interactable
{
    [Header("Display Text")]
    [SerializeField]
    private string description = "Press <color=red>RIGHT CLICK</color>";

    [Space(5)]
    [SerializeField]
    private float timeToTP = 3;

    struct ObjPosSaved
    {
        public GameObject obj;
        public Vector3 rpos;
    }

    private S_Base_Compas_[] BasePlate = new S_Base_Compas_[2];

    List<ObjPosSaved> objBase1 = new List<ObjPosSaved>();
    List<ObjPosSaved> objBase2 = new List<ObjPosSaved>();

    private void Start()
    {
        foreach(Transform t in transform)
        {
            if (t.name == "Base")
            {
                if (BasePlate[0] != null)
                {
                    BasePlate[1] = t.GetComponent<S_Base_Compas_>();
                }
                else
                {
                    BasePlate[0] = t.GetComponent<S_Base_Compas_>();
                }
            }
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
    public override void Interact()
    {
        CancelInvoke();
        Invoke("swapObject", timeToTP);
    }

    private void swapObject()
    {
        foreach (GameObject obj in BasePlate[0].inTrigger) {
            if (obj != null)
            {
                ObjPosSaved temp = new ObjPosSaved();
                temp.obj = obj;
                temp.rpos = obj.transform.position - BasePlate[0].transform.position;
                objBase1.Add(temp);
            }
        }
        foreach (GameObject obj in BasePlate[1].inTrigger)
        {
            if (obj != null)
            {
                ObjPosSaved temp = new ObjPosSaved();
                temp.obj = obj;
                temp.rpos = obj.transform.position - BasePlate[1].transform.position;
                objBase2.Add(temp);
            }
        }

        foreach (ObjPosSaved OPS in objBase1)
        {
            GameObject obj = OPS.obj;
            Vector3 rpos = OPS.rpos;

            obj.transform.position = BasePlate[1].transform.position + rpos;
        }

        foreach (ObjPosSaved OPS in objBase2)
        {
            GameObject obj = OPS.obj;
            Vector3 rpos = OPS.rpos;

            obj.transform.position = BasePlate[0].transform.position + rpos;
        }
        objBase1.Clear();
        objBase2.Clear();
    }

}
