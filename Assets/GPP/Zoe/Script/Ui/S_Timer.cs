using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Timer : MonoBehaviour
{
    public static S_Timer instance;

    public TextMeshProUGUI timerText;
    public float remainingTime;
    public float timeSinceBeggining;

    public Image timerCircle;

    public Gradient grad;

    public Slider slider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        remainingTime = GameMode.instance.settings[0].timePhase;
        //if (slider != null )
        slider.maxValue = remainingTime;
        timerCircle.color = grad.Evaluate(0);
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        timeSinceBeggining += Time.deltaTime;
        if (remainingTime <= 0.1f)
        {
            GameMode.instance.EndGame();
        }
        else
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            //if(slider != null )
            slider.value = remainingTime;
            timerCircle.color = grad.Evaluate(remainingTime/slider.maxValue);
        }

    }

    public void TimerNextPhase(PhaseSettings settings)
    {
        remainingTime = settings.timePhase;
        slider.maxValue = remainingTime;
    }
}
