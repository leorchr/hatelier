using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;

    [HideInInspector] public int currentPhase = 1;
    [HideInInspector] public bool isRunning = true;

    public PhaseSettings[] settings = new PhaseSettings[3];
    public EndStats stats = new EndStats(new int[3], new float[3]);
    public string finalPanelScene = "UIPanelFinal";
    [HideInInspector] public GameObject finalStatue;


    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        startGame();
    }

    public void startGame()
    {
        currentPhase = 1;
        isRunning = true;
        S_InitiateMaterials.instance.setPhase(currentPhase);
    }
    

    public void ChangePhase()
    {
        if (S_ScoreSystem.instance != null && S_Timer.instance != null)
        {
            if (currentPhase == 1)
            {
                S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
                S_Timer.instance.TimerNextPhase(settings[currentPhase]);
                S_Objectives.instance.NextObjective();
            }
            else if (currentPhase == 2)
            {
                S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
                S_Timer.instance.TimerNextPhase(settings[currentPhase]);
                S_Objectives.instance.NextObjective();
            }
            else if (currentPhase == 3)
            {
                S_ScoreSystem.instance.SetupScoreEndPhase(settings[currentPhase - 1]);
                GameMode.instance.stats.timePhases[GameMode.instance.currentPhase - 1] = (int)S_Timer.instance.timeSpendPhase;
                S_Timer.instance.MessageEndWin();
            }


            if(currentPhase < 3) currentPhase++;

            S_InitiateMaterials.instance.setPhase(currentPhase);

            S_ScoreSystem.instance.AddScoreMin();
        }
    }

    public void EndGame()
    {
        if (!isRunning) return;

        for (int i = 0; i < stats.timePhases.Length; i++)
        {
            stats.globalTimeSpend += stats.timePhases[i];
        }
        S_Statue_Inventory.instance.InstantiateAndAssignedStatue();
      
#if UNITY_EDITOR
        //DisplayStats();
#endif
        if (finalPanelScene != null)
        {
            SceneManager.LoadScene(finalPanelScene);
        }
        else
        {
            Debug.LogWarning("Cannot load leaderboard, you must add a name in Final Panel Scene");
        }
        isRunning = false;
    }

#if UNITY_EDITOR

    public void DisplayStats()
    {
        Debug.Log("Score phase 1 : " + stats.scorePhases[0]);
        Debug.Log("Temps phase 1 : " + stats.timePhases[0]);

        Debug.Log("Score phase 2 : " + stats.scorePhases[1]);
        Debug.Log("Temps phase 2 : " + stats.timePhases[1]);

        Debug.Log("Score phase 3 : " + stats.scorePhases[2]);
        Debug.Log("Temps phase 3 : " + stats.timePhases[2]);

        Debug.Log("Global Time Spend : " + stats.globalTimeSpend);
    }


#endif
}


[Serializable]
public struct PhaseSettings {
    public int scoreMin;
    public int scoreMaxAmount;
    public float timePhase;
}


[Serializable]
public struct EndStats { 
    public int[] scorePhases;
    public float[] timePhases;
    public float globalTimeSpend;
    public EndStats(int[] tempScore, float[] tempTimes)
    {
        scorePhases = tempScore;
        timePhases = tempTimes;
        globalTimeSpend = 0;
    }
}