using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Gui : MonoBehaviour {

	public	static Gui		Instance	{ get { return m_Instance; } private set { m_Instance = value; } }
	private	static Gui		m_Instance;

	private	GuiLeaderboard	m_Leaderboard;
	private	Text			m_TextScore;
	private	Text			m_TextGameOver;
	private	Toggle			m_ToggleEasyMode;

	void Awake() {
		m_Instance			= this;

		m_Leaderboard		= this.GetChild<GuiLeaderboard>("Leaderboard");
		m_TextScore			= this.GetChild<Text>("ScoreText");
		m_TextGameOver		= this.GetChild<Text>("TextGameOver");
		m_ToggleEasyMode	= this.GetChild<Toggle>("ToggleEasyMode");

		m_ToggleEasyMode.onValueChanged.AddListener( isOn => { Game.Instance.IsEasyMode = isOn; } );
	}

	public void SetScore(int score) {
		if (m_TextScore != null)
			m_TextScore.text = score.ToString();
	}

	public void ShowGameOver(bool show) {
		if (m_TextGameOver != null)
			m_TextGameOver.gameObject.SetActive(show);
	}

	public void ShowLeaderboard(bool show) {
		if (m_Leaderboard != null) {
			m_Leaderboard.gameObject.SetActive(show);
			m_ToggleEasyMode.gameObject.SetActive(show);
		}
	}
}
