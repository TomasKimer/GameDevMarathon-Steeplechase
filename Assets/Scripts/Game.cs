using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public  static Game Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static Game m_Instance;

    public bool IsPaused { get; set; }
    public bool IsGameOver { get; set; }

    public Leaderboard m_Leaderboard;

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
