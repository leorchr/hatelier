using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UI_PanelFinal : MonoBehaviour
{
    // temps de réalisation
    // game object sculture
    // leader board
    // score

    public static S_UI_PanelFinal instance;

    public GameObject[] nameList, scoreList;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void OnEnable()
    {
        S_LeaderboardSave.instance.SaveToFile();
        S_LeaderboardSave.instance.LoadFile();
    }
}