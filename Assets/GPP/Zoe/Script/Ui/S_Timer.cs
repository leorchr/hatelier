using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remainingTime;
    
    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
