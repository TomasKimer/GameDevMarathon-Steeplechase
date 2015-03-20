using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

    private VerticalLayoutGroup m_List;
    private Text m_TextItem;

    void Awake() {
        m_List = GetComponentInChildren<VerticalLayoutGroup>();
        m_TextItem = m_List.GetComponentInChildren<Text>();

        m_TextItem.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	    GenerateFake();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenerateFake() {
        for (int i = 1; i <= 7; ++i) {
            GameObject newItemGO = Instantiate(m_TextItem.gameObject);
            newItemGO.GetComponent<RectTransform>().SetParent(m_List.gameObject.transform, false);
            
            Text newItem = newItemGO.GetComponent<Text>();
            newItem.text = i + ". " + ((7-i) * 5 + Random.Range(1, 4));

            newItem.gameObject.SetActive(true);
        }
    }

    void OnEnable() {
        
    }

    void OnDisable() {
        
    }
}
