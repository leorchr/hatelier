using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Async Scene", menuName = "ScriptableObjects/New Async Scene")]
public class ASyncScene : ScriptableObject
{
    public string sceneName;
    public int id;
}
