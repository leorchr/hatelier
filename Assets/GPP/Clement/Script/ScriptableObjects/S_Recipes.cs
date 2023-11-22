using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/New Recipe", order = 2)]

public class S_Recipes : ScriptableObject
{
   public static S_Recipes instance;

    private void Awake()
    {
        instance = this;
    }

    [Tooltip("Score donné en récompense de la recette")]
    public int score;
    public Sprite icone;
    [Tooltip("Materiaux requis")]
    public S_Materials[] requiredMaterials;
    [Tooltip("Output statue")]
    public Sprite statueIcon;
    public S_Materials statue;

}
