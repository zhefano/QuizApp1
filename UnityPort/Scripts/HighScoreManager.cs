using System.Collections.Generic;
using UnityEngine;

public static class HighScoreManager
{
    private const string HighScoreKey = "HighScores";
    private const int MaxHighScores = 10;

    public static void SaveHighScore(string playerName, int score)
    {
        var highScores = LoadHighScores();
        highScores.Add(new HighScoreData { PlayerName = playerName, Score = score });
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (highScores.Count > MaxHighScores)
            highScores.RemoveRange(MaxHighScores, highScores.Count - MaxHighScores);
        string json = JsonUtility.ToJson(new HighScoreListWrapper { HighScores = highScores });
        PlayerPrefs.SetString(HighScoreKey, json);
        PlayerPrefs.Save();
    }

    public static List<HighScoreData> LoadHighScores()
    {
        if (!PlayerPrefs.HasKey(HighScoreKey))
            return new List<HighScoreData>();
        string json = PlayerPrefs.GetString(HighScoreKey);
        var wrapper = JsonUtility.FromJson<HighScoreListWrapper>(json);
        return wrapper?.HighScores ?? new List<HighScoreData>();
    }

    public static void ClearHighScores()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
    }

    [System.Serializable]
    private class HighScoreListWrapper
    {
        public List<HighScoreData> HighScores = new List<HighScoreData>();
    }
} 