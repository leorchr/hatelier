using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreSystem : MonoBehaviour
{
    public int score;
    public int scoreMax;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreWithTimer()
    {
        Time.timeScale = 0;
        float endedTime = S_Timer.instance.remainingTime;
        float earnedPoint = endedTime * 100 / S_Timer.instance.maxTime;
        score = (int)earnedPoint % scoreMax;
    }
}
