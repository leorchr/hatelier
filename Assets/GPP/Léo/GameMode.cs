using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;

    [HideInInspector] public int currentPhase = 1;
    public phaseSettings[] settings = new phaseSettings[3];


    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        currentPhase = 1;
    }

    public void ChangePhase()
    {
        if(currentPhase == 1)
        {
            S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase-1]);
            S_Timer.instance.TimerNextPhase(settings[currentPhase]);
        }
        else if(currentPhase == 2)
        {
            S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
            S_Timer.instance.TimerNextPhase(settings[currentPhase]);
        }
        else if (currentPhase == 3)
        {
            S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
            //end game
        }
        currentPhase++;
    }

    public void GameOver()
    {
        // à coder
    }
}

[Serializable]
public struct phaseSettings { public int scoreMin; public int scoreMaxAmount; public float timePhase; }
