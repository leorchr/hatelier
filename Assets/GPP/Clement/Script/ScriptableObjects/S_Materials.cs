using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/New Material", order = 1)]

public class S_Materials : ScriptableObject
{
    public bool canBeBaked = true;
    public Material materials;
    public string materialName;
    [TextAreaAttribute] public string description;
    [TextAreaAttribute] public string FullInventorydescription;
    public Sprite icone;
    public GameObject prefab;
}
