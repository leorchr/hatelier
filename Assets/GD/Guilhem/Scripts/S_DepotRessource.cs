using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DepotRessource : MonoBehaviour
{
    public GameObject objectToSpawn;
    public bool isMaterialPresent;
    [SerializeField] private Transform spawnPosition;

    public void Start()
    {
        SpawnMaterial();
    }

    public void SpawnMaterial()
    {
        if(!isMaterialPresent)
        {
            Instantiate(objectToSpawn, spawnPosition);
        }
    }
}
