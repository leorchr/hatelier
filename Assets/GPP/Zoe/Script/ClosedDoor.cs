using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    [SerializeField] private GameObject m_Door;


    private void Start()
    {
        m_Door.GetComponent<Animator>().SetBool("IsPassed", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Door.GetComponent<Animator>().SetBool("IsPassed", true);
        }
    }
}
