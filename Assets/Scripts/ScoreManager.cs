using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string playerName = "Undefined";

    private static string bestScoreSaveFilePath;
    private BestScoreData bestScoreSaved;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitBestScoreSaved();
    }

    private void InitBestScoreSaved()
    {
        bestScoreSaveFilePath = Application.persistentDataPath + "/bestscore.json";

        bestScoreSaved = GetBestScore();
    }

    private static BestScoreData GetBestScore()
    {
        if (File.Exists(bestScoreSaveFilePath))
        {
            string json = File.ReadAllText(bestScoreSaveFilePath);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);

            return data;
        }
        return new BestScoreData();
    }

    private void SaveBestScore(int score)
    {
        BestScoreData bestScoreData = new BestScoreData();
        bestScoreData.playerName = playerName;
        bestScoreData.score = score;

        string json = JsonUtility.ToJson(bestScoreData);
        File.WriteAllText(bestScoreSaveFilePath, json);
    }

    public string GetBestScoreTitle()
    {
        if (bestScoreSaved.score == 0)
        {
            return "Best Score : 0";
        }
        else
        {
            return "Best Score : " + bestScoreSaved.playerName + " " + bestScoreSaved.score;
        }
    }

    //updt UI best score si besoin
    //save best score si besoin
    public void SuggestNewScore(int newScore)
    {
        if (newScore > bestScoreSaved.score)
        {
            SaveBestScore(newScore);
            bestScoreSaved = GetBestScore();
        }
    }

    [System.Serializable]
    public class BestScoreData
    {
        public string playerName;
        public int score;

        public BestScoreData()
        {
            playerName = "Undefined";
            score = 0;
        }
    }
}
