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
        score = GameMode.instance.settings[0].scoreMin;
    }

    public void SetupScoreEndPhase(phaseSettings settings)
    {
        if(S_Timer.instance != null)
        {
            float endedTime = S_Timer.instance.remainingTime;
            float earnedPoint = endedTime / settings.timePhase * settings.scoreMaxAmount;
            score += (int)earnedPoint;

            score += settings.scoreMin;
        }
    }
}
