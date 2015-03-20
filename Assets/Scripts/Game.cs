using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public  static Game Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static Game m_Instance;

    public bool IsPaused   {
        get {
            return m_IsPaused;
        }
        set {
            if (value != m_IsPaused) {
                m_IsPaused = value;

                Gui.Instance.ShowLeaderboard(m_IsPaused);
            }

            m_IsPaused = value;
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
                    OnGameOver();
            }
        }
    }

    private bool m_IsPaused   = false;
    private bool m_IsGameOver = false;

    void Awake() {
        Debug.Log("Game.Init");

        m_Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            IsPaused = !IsPaused;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }
    }

    void OnGameOver() {
        Gui.Instance.ShowGameOver(true);
    }

    void RestartGame() {
        IsGameOver = false;
        
        Gui.Instance.ShowGameOver(false);
    }
}
