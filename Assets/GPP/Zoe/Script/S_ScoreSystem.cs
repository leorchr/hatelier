using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreSystem : MonoBehaviour
{
    public static S_ScoreSystem instance;

    public int score;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        AddScoreMin();
    }

    public void SetupScoreEndPhase(PhaseSettings settings)
    {
        if (S_Timer.instance != null)
        {
            float endedTime = S_Timer.instance.remainingTime;
            GameMode.instance.stats.timePhases[GameMode.instance.currentPhase - 1] = S_Timer.instance.remainingTime; // ajoute le temps aux stats

            float earnedPoint = endedTime / settings.timePhase * settings.scoreMaxAmount;
            score += (int)earnedPoint;
            GameMode.instance.stats.scorePhases[GameMode.instance.currentPhase - 1] += (int)earnedPoint; // ajoute le score au stats
        }
    }

    public void AddScoreMin()
    {
        score += GameMode.instance.settings[GameMode.instance.currentPhase - 1].scoreMin;
        GameMode.instance.stats.scorePhases[GameMode.instance.currentPhase - 1] += GameMode.instance.settings[GameMode.instance.currentPhase - 1].scoreMin;
    }

    public void AddScore(int scoreBonus)
    {
        score += scoreBonus;
        GameMode.instance.stats.scorePhases[GameMode.instance.currentPhase - 1] += scoreBonus;
    }
}