using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GuiLeaderboard : MonoBehaviour {

    private VerticalLayoutGroup m_List;
    private Text m_TextItem;

    private Leaderboard m_Leaderboard { get { return Game.Instance.Leaderboard; } }

    void Awake() {
        m_List = GetComponentInChildren<VerticalLayoutGroup>();
        m_TextItem = m_List.GetComponentInChildren<Text>();

        m_TextItem.gameObject.SetActive(false);
    }

	void Start () {
	    //GenerateFake();
	}

    void OnEnable() {
        CreateItems();
    }

    void OnDisable() {
        DestroyItems();
    }

    void CreateItems() {
        for (int i = 0; i < m_Leaderboard.Scores.Count; ++i)
            CreateItem(i+1, m_Leaderboard.Scores[i]);
    }

    void CreateItem(int pos, int score) {
        GameObject newItemGO = Instantiate(m_TextItem.gameObject);
        newItemGO.GetComponent<RectTransform>().SetParent(m_List.gameObject.transform, false);
            
        Text newItem = newItemGO.GetComponent<Text>();
        newItem.text = pos + ".    " + score;

        newItem.gameObject.SetActive(true);
    }

    void DestroyItems() {
        Text[] items = m_List.GetComponentsInChildren<Text>(true);

        foreach (Text item in items) {
            if (item.name.Equals("TextItem"))
                continue;
            
            Object.Destroy(item.gameObject);
        }
    }

    void GenerateFake() {
        for (int i = 1; i <= 7; ++i) {
            CreateItem(i, (7-i) * 5 + Random.Range(1, 4));
        }
    }
}
