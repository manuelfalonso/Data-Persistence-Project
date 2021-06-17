using System;
using System.IO;
using UnityEngine;

public class HighScoreManager : Singleton<HighScoreManager>
{
    public string actualPlayer;
    public string highScorePlayer;
    public int highScore;

    void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
    }

    public void SetActualPlayer(string newPlayer)
    {
        actualPlayer = newPlayer;
    }

    internal void SetHighScorePlayer(int newHighScore)
    {
        highScorePlayer = actualPlayer;
        highScore = newHighScore;
        SaveHighScore();
        Events.Instance.NewHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string highScorePlayer;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScorePlayer = highScorePlayer;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayer = data.highScorePlayer;
            highScore = data.highScore;
        }
    }
}
