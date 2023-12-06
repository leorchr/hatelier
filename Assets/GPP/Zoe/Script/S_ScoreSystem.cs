using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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
        float endedTime = S_Timer.instance.remainingTime;
        float earnedPoint = endedTime / settings.timePhase * settings.scoreMaxAmount;
        Debug.Log(earnedPoint);
        score += (int)earnedPoint;
        Debug.Log(score);

        score += settings.scoreMin;
        Debug.Log(score);
    }
}
