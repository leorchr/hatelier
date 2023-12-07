using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Managers : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
