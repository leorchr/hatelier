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
    
    // Display Stats Player
    [SerializeField] private TextMeshProUGUI nameText;
    [HideInInspector] public int playerScoreTemp = 0;
    [HideInInspector] public string playerNameTemp = "Name";
    [HideInInspector] public float playerTimeTemp = 0.0f;

    private GameObject[] display;
    private GameObject[] nameList;
    private GameObject[] scoreList;
    private GameObject[] timeList;


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
    }

    public void SetupLeaderboard()
    {
        playerNameTemp = nameText.GetComponent<TextMeshProUGUI>().text;
        playerScoreTemp = S_ScoreSystem.instance.score;
        playerTimeTemp = S_Timer.instance.timeSinceBeggining;

        S_LeaderboardSave.instance.LoadFile();
        S_LeaderboardSave.instance.SaveToFile();
        S_LeaderboardSave.instance.saveData.UpdateUI();

        leaderboard.SetActive(true);
        inputField.SetActive(false);
    }

    public GameObject[] GetNameList() { return nameList; }
    public GameObject[] GetScoreList() { return scoreList; }
    public GameObject[] GetTimeList() { return timeList; }
}