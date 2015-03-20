using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

    public  static Gui Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static Gui m_Instance;

    public Leaderboard leaderboard;

    void Awake() {
        m_Instance = this;

        leaderboard.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
