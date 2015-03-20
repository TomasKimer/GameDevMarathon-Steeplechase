﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Gui : MonoBehaviour {

    public  static Gui Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static Gui m_Instance;

    private Leaderboard m_Leaderboard;
    private Text m_TextGameOver;

    void Awake() {
        m_Instance = this;

        m_Leaderboard  = this.GetChild<Leaderboard>("Leaderboard");
        m_TextGameOver = this.GetChild<Text>("TextGameOver");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowGameOver(bool show) {
        if (m_TextGameOver != null)
            m_TextGameOver.gameObject.SetActive(show);
    }

    public void ShowLeaderboard(bool show) {
        if (m_Leaderboard != null)
            m_Leaderboard.gameObject.SetActive(show);
    }

}