using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public GameObject wallPrefab;

	// Use this for initialization
	void Start () {

		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);
		InstantiateWall (-15, 15);

		wallPrefab.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstantiateWall(int minPos, int maxPos){
	
		Instantiate(wallPrefab, new Vector3(Random.Range(minPos, maxPos), Random.Range(minPos, maxPos), 0), 
		            Quaternion.Euler (0, 0, (Random.Range(0, 180))));

	
	}
}
