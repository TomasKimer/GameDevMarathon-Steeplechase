using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInit : MonoBehaviour
{

	public GameObject enemyObject;
	private Enemy[] enemies = new Enemy[10];

	// Use this for initialization
	void Start ()
	{
		//for(int i = 0; i < 10; i++) {
		enemies [0] = Instantiate (enemyObject, new Vector3 (20, 0, 1), Quaternion.identity) as Enemy;
		//}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
