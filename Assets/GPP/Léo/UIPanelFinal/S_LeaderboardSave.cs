using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class S_LeaderboardSave : MonoBehaviour
{
    public static S_LeaderboardSave instance;

    [HideInInspector] public SaveData saveData;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void Start() { }

    public void SaveToFile()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            saveData.Save();
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath + "/data.save", json);
        }

        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            NewSave();
        }
    }

    public void NewSave()
    {
        saveData = new SaveData();
        saveData.Save();
        string json = JsonUtility.ToJson(saveData);
        File.Create(Application.persistentDataPath + "/data.save").Dispose();
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);
    }


    public void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            saveData = JsonUtility.FromJson<SaveData>(json);
            saveData.UpdateUI();
        }
        else
        {
            saveData = new SaveData();
        }
    }
}

[Serializable]
public class SaveData
{
    public List<PlayerStats> stats = new List<PlayerStats>();

    public void Save()
    {
        stats.Add(new PlayerStats(S_Leaderboard.instance.playerNameTemp, S_Leaderboard.instance.playerScoreTemp, S_Leaderboard.instance.playerTimeTemp));
    }
    public void UpdateUI()
    {
        List<PlayerStats> ranked;
        ranked = stats.OrderBy(s => s.playerScore).ToList();
        ranked.Reverse();
        
        for (int i = 0; i < S_Leaderboard.instance.numberPlayersDisplay; i++)
        {
            if (i >= ranked.Count)
            {
                S_Leaderboard.instance.GetNameList()[i].GetComponent<TextMeshProUGUI>().text = "";
                S_Leaderboard.instance.GetScoreList()[i].GetComponent<TextMeshProUGUI>().text = "";
                S_Leaderboard.instance.GetTimeList()[i].GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                S_Leaderboard.instance.GetNameList()[i].GetComponent<TextMeshProUGUI>().text = ranked[i].playerName;
                S_Leaderboard.instance.GetScoreList()[i].GetComponent<TextMeshProUGUI>().text = ranked[i].playerScore.ToString();
                float time = ranked[i].playerTime;
                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                S_Leaderboard.instance.GetTimeList()[i].GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}

[Serializable]
public struct PlayerStats
{
    public string playerName;
    public int playerScore;
    public float playerTime;

    public PlayerStats(string name, int score, float time)
    {
        playerName = name;
        playerScore = score;
        playerTime = time;
    }
}