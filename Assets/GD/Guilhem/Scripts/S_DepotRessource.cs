using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DepotRessource : MonoBehaviour
{
    public GameObject objectToSpawn;
    [SerializeField] private S_Interactable_Obj objectToSpawnScript;
    public bool isMaterialPresent;
    public float respawnTime = 2f;
    public bool isObject;
    [SerializeField] private Transform spawnPosition;
    public GameObject objectOnDepot;
    public bool objectHasDispawn;

    //private float timerSpawnCurrent;
    //[SerializeField] private float timerSpawnMax;

    public void Start()
    {
        respawnTime = 2f;
        if(objectToSpawn != null)
        objectToSpawnScript = objectToSpawn.GetComponent<S_Interactable_Obj>();

        SpawnMaterial();
        
    }

    public void Update()
    {
        CheckMaterialPresence();
        if(objectOnDepot == null && objectHasDispawn)
        {
            Invoke("SpawnMaterial", respawnTime);
            objectHasDispawn = false;
        }
    }

    public void SpawnMaterial()
    {
        if(!isMaterialPresent && objectToSpawn != null)
        {
            objectOnDepot = Instantiate(objectToSpawn, spawnPosition);
            isMaterialPresent = true;
            objectToSpawn.GetComponent<S_Interactable_Obj>().isObj = true;
            objectHasDispawn = true;
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
