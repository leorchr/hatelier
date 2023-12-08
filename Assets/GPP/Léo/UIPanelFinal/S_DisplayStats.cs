using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class S_DisplayStats : MonoBehaviour
{
    [SerializeField] private GameObject playerStats;
    [SerializeField] private GameObject spawnPos;
    void Start()
    {
        int minutes, seconds;
        playerStats = Instantiate(playerStats, spawnPos.transform);
        for (int i = 0; i < 3; i++)
        {
            playerStats.transform.GetChild(i+1).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = GameMode.instance.stats.scorePhases[i].ToString();
            minutes = Mathf.FloorToInt(GameMode.instance.stats.timePhases[i] / 60);
            seconds = Mathf.FloorToInt(GameMode.instance.stats.timePhases[i] % 60);
            playerStats.transform.GetChild(i+1).transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        playerStats.transform.GetChild(4).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Score : " + S_ScoreSystem.instance.score.ToString();
        float time = S_Timer.instance.timeSinceBeggining;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        playerStats.transform.GetChild(4).transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Temps total : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
