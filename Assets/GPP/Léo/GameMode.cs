using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;

    [HideInInspector] public int currentPhase = 1;
    public phaseSettings[] settings = new phaseSettings[3];
    public string finalPanelScene = "UIPanelFinal";


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
        if(S_ScoreSystem.instance != null && S_Timer.instance != null)
        {
            if (currentPhase == 1)
            {
                S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
                S_Timer.instance.TimerNextPhase(settings[currentPhase]);
            }
            else if (currentPhase == 2)
            {
                S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
                S_Timer.instance.TimerNextPhase(settings[currentPhase]);
            }
            else if (currentPhase == 3)
            {
                S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
                EndGame();
            }

            currentPhase++;
        }
    }

    public void EndGame()
    {
        if (finalPanelScene != null)
        {
            SceneManager.LoadScene(finalPanelScene);
        }
        else
        {
            Debug.LogWarning("Cannot load leaderboard, you must add a name in Final Panel Scene");
        }
    }
}

[Serializable]
public struct phaseSettings { public int scoreMin; public int scoreMaxAmount; public float timePhase; }