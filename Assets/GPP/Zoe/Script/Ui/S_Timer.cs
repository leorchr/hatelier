using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Timer : MonoBehaviour
{
    public static S_Timer instance;

    public TextMeshProUGUI timerText;
    public float remainingTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        remainingTime = GameMode.instance.settings[0].timePhase;
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if(remainingTime <= 0)
        {
            GameMode.instance.GameOver();
        }
        else
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    public void TimerNextPhase(phaseSettings settings)
    {
        remainingTime = settings.timePhase;
    }
}
