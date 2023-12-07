using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DepotRessource : MonoBehaviour
{
    public GameObject objectToSpawn;
    [SerializeField] private S_Interactable_Obj objectToSpawnScript;
    public bool isMaterialPresent;
    public float respawnTime;
    public bool isObject;
    [SerializeField] private Transform spawnPosition;

    private float timerSpawnCurrent;
    [SerializeField] private float timerSpawnMax;

    public void Start()
    {
        if(objectToSpawn != null)
        objectToSpawnScript = objectToSpawn.GetComponent<S_Interactable_Obj>();

        SpawnMaterial();

    }

    public void Update()
    {
        CheckMaterialPresence();

        if(!isObject)
        {
            Invoke("SpawnMaterial", respawnTime);
        }
    }

    public void SpawnMaterial()
    {
        if(!isMaterialPresent && objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPosition);
            isMaterialPresent = true;
            isObject = true;

            objectToSpawnScript.depotRessource = this;
            //timerSpawnCurrent = timerSpawnMax;
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
