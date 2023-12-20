using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Leaderboard : MonoBehaviour
{
    public static S_Leaderboard instance;

    [Min(0)] public int numberPlayersDisplay = 3;
    [Space]
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject inputField;
    [SerializeField] private GameObject leaderboardText;

    // Display Stats Player
    [SerializeField] private TextMeshProUGUI nameText;
    [HideInInspector] public int playerScoreTemp = 0;
    [HideInInspector] public string playerNameTemp = "Name";
    [HideInInspector] public float playerTimeTemp = 0.0f;

    private GameObject[] display;
    private GameObject[] nameList;
    private GameObject[] scoreList;
    private GameObject[] timeList;
    [SerializeField] private Transform statuePos;

    [SerializeField] private TextMeshProUGUI scoreText, timeText;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        display = new GameObject[numberPlayersDisplay];
        nameList = new GameObject[numberPlayersDisplay];
        scoreList = new GameObject[numberPlayersDisplay]; 
        timeList = new GameObject[numberPlayersDisplay];

        for (int i = 0; i < numberPlayersDisplay; i++)
        {
            display[i] = Instantiate(displayPrefab, leaderboard.transform);
            nameList[i] = display[i].transform.GetChild(0).gameObject;
            scoreList[i] = display[i].transform.GetChild(1).gameObject;
            timeList[i] = display[i].transform.GetChild(2).gameObject;
        }

        if(GameMode.instance.finalStatue != null) Instantiate(GameMode.instance.finalStatue, statuePos.transform);

        scoreText.text = S_ScoreSystem.instance.score.ToString();
        float timeTemp = GameMode.instance.stats.globalTimeSpend;
        int minutes = Mathf.FloorToInt(timeTemp / 60);
        int seconds = Mathf.FloorToInt(timeTemp % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetupLeaderboard()
    {
        string currentName = nameText.GetComponent<TextMeshProUGUI>().text;
        if (currentName.Length < 4) return;
        playerNameTemp = nameText.GetComponent<TextMeshProUGUI>().text;
        playerScoreTemp = S_ScoreSystem.instance.score;
        playerTimeTemp = GameMode.instance.stats.globalTimeSpend;

        S_LeaderboardSave.instance.LoadFile();
        S_LeaderboardSave.instance.SaveToFile();
        S_LeaderboardSave.instance.saveData.UpdateUI();

        S_DisplayStats.instance.DisableStats();
        leaderboardText.SetActive(true);
        leaderboard.SetActive(true);
        inputField.SetActive(false);

    }

    public GameObject[] GetNameList() { return nameList; }
    public GameObject[] GetScoreList() { return scoreList; }
    public GameObject[] GetTimeList() { return timeList; }
}