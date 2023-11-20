using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/New Recipe", order = 2)]

public class S_Recipes : ScriptableObject
{
    public S_Materials[] requiredMaterials;
    [Tooltip("Score donné en récompense de la recette")]
    public int score;
    public Sprite icone;
}
