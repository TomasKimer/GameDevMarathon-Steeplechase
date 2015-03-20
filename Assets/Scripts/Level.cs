﻿using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public GameObject wallPrefab;
    public GameObject groundPrefab;

    [Range(0.0f, 1.0f)]
    public float tintInterval = 0.12f;

    private SpriteRenderer[] m_groundSprites;

    void Awake() {
        m_groundSprites = groundPrefab.GetComponentsInChildren<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {

		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);

		wallPrefab.SetActive (false);

        StartCoroutine( _TintRepeated() );
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

	void InstantiateWall(int minPos, int maxPos){
	
		Instantiate(wallPrefab, new Vector3(Random.Range(minPos, maxPos), Random.Range(minPos, maxPos), 0), 
		            Quaternion.Euler (0, 0, (Random.Range(0, 180))));
	}

    void SetTint(Color color) {
        foreach (SpriteRenderer sr in m_groundSprites) {
            sr.color = color;
        }
    }

    IEnumerator _TintRepeated() {
        for (;;) {
            SetTint(new Color(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f)));
            yield return new WaitForSeconds(tintInterval);
        }
    }
}
