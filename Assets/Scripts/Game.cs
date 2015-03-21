using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

	public  static Game Instance { get { return m_Instance; } private set { m_Instance = value; } }
	private static Game m_Instance;

	[Range(0.1f, 5.0f)]
	public  float
		scoreUpdateInterval = 1f;



	public PowerupManager PowerupManager { get; private set; }
	public Leaderboard Leaderboard { get { return m_Leaderboard; } }
    
	public int  CurrentLevel { get; set; }
	public bool IsEasyMode   { get; set; }


	public System.Action<bool> GamePaused;
	public System.Action       GameRestart;
    
	private int m_Score = 0;
	private Leaderboard m_Leaderboard = new Leaderboard ();

	public bool IsPaused {
		get {
			return m_IsPaused;
		}
		set {
			if (value != m_IsPaused) {
				m_IsPaused = value;

				if (GamePaused != null)
					GamePaused (m_IsPaused);

				Gui.Instance.ShowLeaderboard (m_IsPaused);
                
				if (Camera.main != null)
					Camera.main.GetComponentInParent<CameraController> ().enabled = !m_IsPaused;
			}
		}
	}

	public bool IsGameOver {
		get {
			return m_IsGameOver;
		}
		set {
			if (value != m_IsGameOver) {
				m_IsGameOver = value;

				if (m_IsGameOver)
					OnGameOver ();

				if (Camera.main != null)
					Camera.main.GetComponentInParent<CameraController> ().enabled = !m_IsGameOver;
			}
		}
	}

	private bool m_IsPaused = false;
	private bool m_IsGameOver = false;

	void Awake ()
	{
		Debug.Log ("Game.Init");

		m_Instance = this;

		PowerupManager = GetComponent<PowerupManager> ();
		m_Leaderboard.Load ();
	}

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (_UpdateScore ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			IsPaused = !IsPaused;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			RestartGame ();
		}
	}

	void OnGameOver ()
	{
		m_Leaderboard.Add (m_Score);

		Gui.Instance.ShowGameOver (true);
	}

	void RestartGame ()
	{
		IsGameOver = false;
		CurrentLevel = 0;
        
		Gui.Instance.SetScore (m_Score = 0);
        
		Gui.Instance.ShowGameOver (false);

		if (GameRestart != null)
			GameRestart ();
	}

	IEnumerator _UpdateScore ()
	{
		while (true) {
			yield return new WaitForSeconds (scoreUpdateInterval);
            
			if (!IsPaused && !IsGameOver) {
				Gui.Instance.SetScore (++m_Score);

				int newLevel = m_Score / 10;

				if (newLevel > CurrentLevel) {
					CurrentLevel = newLevel;
					Debug.Log ("Level UP: " + CurrentLevel);
				}
			}
		}
	}
}
