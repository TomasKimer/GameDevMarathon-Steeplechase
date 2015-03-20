using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public  static GameController Instance { get { return m_Instance; } private set { m_Instance = value; } }
    private static GameController m_Instance;

    void Awake() {
        Debug.Log("GameController Init");

        m_Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
