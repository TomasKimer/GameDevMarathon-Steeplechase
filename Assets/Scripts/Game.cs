using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public  static Game Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static Game m_Instance;

    [Range(0.1f, 5.0f)]
    public  float scoreUpdateInterval = 1f;

    public System.Action<bool> OnGamePaused;
    
    private int m_Score = 0;

    public bool IsPaused   {
        get {
            return m_IsPaused;
        }
        set {
            if (value != m_IsPaused) {
                m_IsPaused = value;

                if (OnGamePaused != null)
                    OnGamePaused(m_IsPaused);

                Gui.Instance.ShowLeaderboard(m_IsPaused);

                GameObject.Find("/Camera").GetComponent<CameraController>().enabled = !m_IsPaused;
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
                    OnGameOver();

                GameObject.Find("/Camera").GetComponent<CameraController>().enabled = !m_IsGameOver;
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
        StartCoroutine( _UpdateScore() );
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
        
        Gui.Instance.SetScore(m_Score = 0);
        
        Gui.Instance.ShowGameOver(false);
    }

    IEnumerator _UpdateScore() {
        while (true) {
            yield return new WaitForSeconds(scoreUpdateInterval);
            
            if (!IsPaused && !IsGameOver)
            {
                Gui.Instance.SetScore(++m_Score);
            }
        }
    }
}
