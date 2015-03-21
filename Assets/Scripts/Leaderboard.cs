using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Leaderboard {

    const int    MAX_ITEMS         = 7;
    const string PLAYERPREFS_KEY   = "Leaderboard";
    const char   PLAYERPREFS_DELIM = '|';

    public List<int> Scores { get { return m_Scores; } }

    private List<int> m_Scores = new List<int>(MAX_ITEMS);

    public void Add(int score) {
        m_Scores.Add(score);
        m_Scores.Sort((a, b) => -a.CompareTo(b));
        
        if (m_Scores.Count > MAX_ITEMS) {
            m_Scores.RemoveRange(MAX_ITEMS - 1, m_Scores.Count - MAX_ITEMS);
        }
        
        Save();
    }

    public void Save() {
        StringBuilder builder = new StringBuilder();

        foreach (int score in m_Scores) {
            builder.Append(score.ToString());
            builder.Append(PLAYERPREFS_DELIM);
        }
        
        PlayerPrefs.SetString(PLAYERPREFS_KEY, builder.ToString());
        PlayerPrefs.Save();
    }

    public void Load() {
        string   scoresStr = PlayerPrefs.GetString(PLAYERPREFS_KEY, "");
        string[] scoresStrArr = scoresStr.Split(PLAYERPREFS_DELIM);

        m_Scores.Clear();

        foreach (string score in scoresStrArr) {
            int intScore;
            
            if (string.IsNullOrEmpty(score) || !int.TryParse(score, out intScore))
                continue;
            
            m_Scores.Add(intScore);
        }
    }
}
