using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public  static Game Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static Game m_Instance;

    public Leaderboard m_Leaderboard;

    public bool IsPaused   {
        get {
            return m_IsPaused;
        }
        set {
            m_IsPaused = value;
        }
    }

    public bool IsGameOver {
        get {
            return m_IsGameOver;
        }
        set {
            m_IsGameOver = value;
        }
    }

    private bool m_IsPaused   = false;
    private bool m_IsGameOver = false;

    void Awake() {
        Debug.Log("Game.Init");

        m_Instance = this;

        m_Leaderboard.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            IsPaused = !IsPaused;

            if (IsPaused)
                m_Leaderboard.gameObject.SetActive(true);
            else
                m_Leaderboard.gameObject.SetActive(false);
        }
	}
}
