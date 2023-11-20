using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class S_Base_Compas_ : MonoBehaviour
{
    public List<GameObject> inTrigger;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object In");
        if (other.tag == "Player")
        {
            inTrigger.Add(other.gameObject);
        }
        else if (other.GetComponent<S_Interactable_Obj>() != null)
        {
            inTrigger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object out");
        if (inTrigger.Contains(other.gameObject))
        {
            inTrigger.Remove(other.gameObject);
        }
    }

}
