using UnityEngine;
using UnityEngine.UIElements;

public class S_RandomSpawnMaterials : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] materials;

    private void Start()
    {
        foreach (Transform pos in spawnPositions)
        {
            GameObject randMat = materials[Random.Range(0, materials.Length)];
            Instantiate(randMat, pos);
        }
    }
}