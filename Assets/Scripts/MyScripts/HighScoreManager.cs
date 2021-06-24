using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighScoreManager : Singleton<HighScoreManager>
{
    public string actualPlayer;
    public string bestHighScorePlayer;
    public int bestHighScore;

    protected int maxScoreQuantity = 10;

    private SaveDataList highScoreTable;

    void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);

        LoadHighScores();

        if (highScoreTable == null)
        {
            highScoreTable = new SaveDataList();
        }
    }

    public void SetActualPlayer(string newPlayer)
    {
        actualPlayer = newPlayer;
    }

    internal void SetHighScorePlayer(int newHighScore)
    {
        highScoreTable.Add(actualPlayer, newHighScore);

        SaveHighScores();
    }

    void SetBestHighScorePlayer(int newHighScore)
    {
        bestHighScorePlayer = actualPlayer;
        bestHighScore = newHighScore;

        highScoreTable.Add(actualPlayer, newHighScore);

        SaveHighScores();
        Events.Instance.NewHighScore();
    }

    void LoadBestHighScorePlayer()
    {
        if (highScoreTable.highScorePoints.Count != 0)
        {
            bestHighScorePlayer = highScoreTable.highScorePlayer[0];
            bestHighScore = highScoreTable.highScorePoints[0];
        }
        else
        {
            bestHighScorePlayer = "";
            bestHighScore = 0;
        }
    }

    public void CheckHighScore(int scoreToCheck)
    {        
        if (highScoreTable.highScorePoints.Count == 0 || scoreToCheck > highScoreTable.GetMaxScore())
        {
            SetBestHighScorePlayer(scoreToCheck);
        }
        else
        {
            SetHighScorePlayer(scoreToCheck);
        }
    }

    public void PrintHighScoreTable()
    {
        Debug.Log("highScoreTable.Length " + highScoreTable.highScorePoints.Count);

        for (int i = 0; i < highScoreTable.highScorePoints.Count; i++)
        {
            Debug.Log("[" + (i + 1) + "] " 
                + highScoreTable.highScorePlayer[i]
                + ":"
                + highScoreTable.highScorePoints[i]
            );
        }
    }

    public string HighScoreTableToString()
    {
        return highScoreTable.HighScoresToString();
    }

    [System.Serializable]
    class SaveDataList
    {
        public List<string> highScorePlayer;
        public List<int> highScorePoints;

        public SaveDataList()
        {
            highScorePlayer = new List<string>();
            highScorePoints = new List<int>();
        }

        public int GetMaxScore()
        {
            return highScorePoints.Max();
        }

        public void Add(string newScorePlayer, int newScorePoints)
        {
            if (highScorePlayer.Count == 0)
            {
                highScorePlayer.Add(newScorePlayer);
                highScorePoints.Add(newScorePoints);
            }
            else
            {
                int insertIndex = -1;
                for (int i = 0; i < highScorePoints.Count; i++)
                {
                    if (newScorePoints > highScorePoints[i])
                    {
                        insertIndex = i;
                        break;
                    }
                }

                if (insertIndex == -1 && highScorePoints.Count < HighScoreManager.Instance.maxScoreQuantity)
                {
                    // Free space to add the lowest high score
                    highScorePlayer.Add(newScorePlayer);
                    highScorePoints.Add(newScorePoints);
                }
                else if (insertIndex != -1)
                {
                    // Insert the new high score in the position above a lower score
                    highScorePlayer.Insert(insertIndex, newScorePlayer);
                    highScorePoints.Insert(insertIndex, newScorePoints);
                }

                // Remove scores that get lower of the top Maximun scores
                if (highScorePoints.Count > HighScoreManager.Instance.maxScoreQuantity)
                {
                    highScorePlayer.RemoveAt(HighScoreManager.Instance.maxScoreQuantity);
                    highScorePoints.RemoveAt(HighScoreManager.Instance.maxScoreQuantity);
                }
            }
        }

        public void PrintHighScores()
        {
            Debug.Log("Cantidad: " + highScorePlayer.Count);

            for (int i = 0; i < highScorePlayer.Count; i++)
            {
                Debug.Log("[" + i + "] " + highScorePoints[i] + " " + highScorePlayer[i]);
            }
        }

        public string HighScoresToString()
        {
            string scoreTable = "";
            for (int i = 0; i < highScorePlayer.Count; i++)
            {
                scoreTable += "[" + (i + 1) + "] Score: " + highScorePoints[i] + ", Player: " + highScorePlayer[i] + "\n";
            }
            return scoreTable;
        }
    }

    public void SaveHighScores()
    {
        SaveDataList data = highScoreTable;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataList data = JsonUtility.FromJson<SaveDataList>(json);

            highScoreTable = data;
        }

        // Load Best High Scores to show in the menu
        LoadBestHighScorePlayer();
    }

    public void ResetHighScores()
    {
        SaveDataList data = new SaveDataList();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        // Load that empty HighScores
        LoadHighScores();
        // Load Best High Scores to show in the menu
        LoadBestHighScorePlayer();
    }
}
