using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class S_Leaderboard : MonoBehaviour
{
    public static S_Leaderboard instance;

    [Min(0)] public int numberPlayersDisplay = 3;
    [Space]
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject inputField;
    [SerializeField] private GameObject text;
    
    // Display Stats Player
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    private GameObject[] display;
    private GameObject[] nameList;
    private GameObject[] scoreList;

    [HideInInspector] public int playerScoreTemp = 0;
    [HideInInspector] public string playerNameTemp = "Name";

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        display = new GameObject[numberPlayersDisplay];
        nameList = new GameObject[numberPlayersDisplay];
        scoreList = new GameObject[numberPlayersDisplay];

        for (int i = 0; i < numberPlayersDisplay; i++)
        {
            display[i] = Instantiate(displayPrefab, leaderboard.transform);
            nameList[i] = display[i].transform.GetChild(0).gameObject;
            scoreList[i] = display[i].transform.GetChild(1).gameObject;
        }

        scoreText.text = "Score : " + S_ScoreSystem.instance.score.ToString();
        float time = S_Timer.instance.timeSinceBeggining;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = "Temps total : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetupLeaderboard()
    {
        playerNameTemp = text.GetComponent<TextMeshProUGUI>().text;
        playerScoreTemp = S_ScoreSystem.instance.score; // récupérer le score du joueur ici
        S_LeaderboardSave.instance.LoadFile();
        S_LeaderboardSave.instance.SaveToFile();
        S_LeaderboardSave.instance.saveData.UpdateUI();
        leaderboard.SetActive(true);
        inputField.SetActive(false);
    }

    public GameObject[] GetNameList() { return nameList; }
    public GameObject[] GetScoreList() { return scoreList; }
}