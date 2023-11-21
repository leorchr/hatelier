using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class S_LeaderboardSave : MonoBehaviour
{
    public static S_LeaderboardSave instance;

    [HideInInspector] public SaveData saveData;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        LoadFile();
    }

    public void SaveToFile()
    {
        saveData.Save();
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Create(Application.persistentDataPath + "/data.save").Dispose();
        }
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);
    }

    public void NewSave()
    {
        saveData = new SaveData();
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
        File.Create(Application.persistentDataPath + "/data.save").Dispose();
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);
    }


    public void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            saveData = JsonUtility.FromJson<SaveData>(json);
            saveData.Load();
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
    public string playerName = "Titouan";
    public int score = 0;

    public void Save()
    {
        // récupérer le score du joueur ici
        // récupérer le nom du joueur ici
    }
    public void Load()
    {
        S_UI_PanelFinal.instance.nameList[0].GetComponent<TextMeshProUGUI>().text = playerName;
        S_UI_PanelFinal.instance.scoreList[0].GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}