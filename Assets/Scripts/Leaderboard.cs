using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leaderboard {

    const int MAX_ITEMS = 7;

    public List<int> Scores { get { return m_Scores; } }

    private List<int> m_Scores = new List<int>(MAX_ITEMS);

    public void Add(int score) {
        m_Scores.Add(score);
    }

    void Save() {
        
    }

    void Load() {

    }
}
