using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public  static GameController Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static GameController m_Instance;

    public bool IsPaused { get; set; }

    public Leaderboard m_Leaderboard;

    void Awake() {
        Debug.Log("GameController Init");

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
