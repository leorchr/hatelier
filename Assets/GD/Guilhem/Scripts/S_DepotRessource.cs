using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DepotRessource : MonoBehaviour
{
    public GameObject objectToSpawn;
    public bool isMaterialPresent;
    [SerializeField] private Transform spawnPosition;

    private float timerSpawnCurrent;
    [SerializeField] private float timerSpawnMax;

    public void Start()
    {
        SpawnMaterial();
    }

    public void Update()
    {
        CheckMaterialPresence();
    }

    public void SpawnMaterial()
    {
        if(!isMaterialPresent && objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPosition);
            isMaterialPresent = true;

            timerSpawnCurrent = timerSpawnMax;
        }
    }

    public void CheckMaterialPresence()
    {
        if(objectToSpawn != null)
        {
            Transform scriptMaterial = objectToSpawn.GetComponent<Transform>();
            if (scriptMaterial.transform.position != spawnPosition.position)
            {
                isMaterialPresent = false;
            }
        }
    }
}
