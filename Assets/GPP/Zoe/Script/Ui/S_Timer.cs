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
    public float maxTime;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        maxTime = remainingTime;
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
