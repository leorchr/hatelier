using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InitiateMaterials : MonoBehaviour
{
    public static S_InitiateMaterials instance;

    [Header("First Step")]
    [SerializeField] private List<GameObject> firstStepDepotsObjects;
    [SerializeField] private List<GameObject> firstStepMaterials;

    [Header("Second Step")]
    [SerializeField] private List<GameObject> secondStepDepots;
    [SerializeField] private List<GameObject> secondStepMaterials;

    [Header("Third Step")]
    [SerializeField] private List<GameObject> thirdStepDepots;
    [SerializeField] private List<GameObject> thirdStepMaterials;

    public void setPhase(int i)
    {
        switch(i)
        {
            case 1:
                setCanSpawnDepot(firstStepDepotsObjects, true);
                setCanSpawnDepot(secondStepDepots, false);
                setCanSpawnDepot(thirdStepDepots, false);
                break;
            case 2:
                setCanSpawnDepot(firstStepDepotsObjects, false);
                setCanSpawnDepot(secondStepDepots, true);
                setCanSpawnDepot(thirdStepDepots, false);
                break;
            case 3:
                setCanSpawnDepot(firstStepDepotsObjects, false);
                setCanSpawnDepot(secondStepDepots, false);
                setCanSpawnDepot(thirdStepDepots, true);
                break;
            default:
                break;
        }
    }

    void setCanSpawnDepot(List<GameObject> l, bool b)
    {
        foreach(GameObject go in l)
        {
            go.GetComponent<S_DepotRessource>().setCanSpawn(b);
        }
    }

    void Awake()
    {
        if (!instance) instance = this;

        if (firstStepDepotsObjects.Count != 0)
        {
            foreach (GameObject depot in firstStepDepotsObjects)
            {
                if (firstStepMaterials.Count != 0)
                {
                    int randomMaterial = Random.Range(0, firstStepMaterials.Count);
                    depot.GetComponent<S_DepotRessource>().objectToSpawn = firstStepMaterials[randomMaterial];

                    firstStepMaterials.Remove(firstStepMaterials[randomMaterial]);
                }
            }
        }

        if (secondStepDepots.Count != 0)
        {
            foreach (GameObject depot in secondStepDepots)
            {
                if (secondStepMaterials.Count != 0)
                {
                    int randomMaterial = Random.Range(0, secondStepMaterials.Count);
                    depot.GetComponent<S_DepotRessource>().objectToSpawn = secondStepMaterials[randomMaterial];

                    secondStepMaterials.Remove(secondStepMaterials[randomMaterial]);
                }
            }

        }

        if (thirdStepDepots.Count != 0)
        {
            foreach (GameObject depot in thirdStepDepots)
            {
                if (thirdStepMaterials.Count != 0)
                {
                    int randomMaterial = Random.Range(0, thirdStepMaterials.Count);
                    depot.GetComponent<S_DepotRessource>().objectToSpawn = thirdStepMaterials[randomMaterial];

                    thirdStepMaterials.Remove(thirdStepMaterials[randomMaterial]);

                }
            }
        }
    }
}
