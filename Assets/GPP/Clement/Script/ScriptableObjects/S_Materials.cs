using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/New Material", order = 1)]

public class S_Materials : ScriptableObject
{
    public bool canBeBaked = true;
    public Material materials;
    public string materialName;
    public string displayName;
    public Texture2D baseColor1;
    public Texture2D normal1;
    public Texture2D baseColor2;
    public Texture2D normal2;
    public Texture2D baseColor3;
    public Texture2D normal3;
    [TextAreaAttribute] public string description;
    [TextAreaAttribute] public string FullInventorydescription;
    public Sprite icone;
    public GameObject armature;
    public GameObject prefab;
    public MaterialPhase phase;
}

public enum MaterialPhase{
    Phase1,
    Phase2,
    Phase3
}