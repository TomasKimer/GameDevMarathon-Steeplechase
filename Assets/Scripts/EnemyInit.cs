﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInit : MonoBehaviour
{

	public GameObject enemyObject;
	public int nrEnemies = 50;
	public int spawnDistance = 1;
	public int spawnRate = 2;

	// Use this for initialization
	void Start ()
	{
		Game.Instance.GameRestart += OnGameRestart;
		StartCoroutine ("spawnEnemies");
	}

	void OnGameRestart ()
	{
		Enemy[] enemies = FindObjectsOfType (typeof(Enemy)) as Enemy[];
		foreach (Enemy enemy in enemies) {
			Destroy (enemy.gameObject);
		}

		StopCoroutine ("spawnEnemies");
		StartCoroutine ("spawnEnemies");
	}
	
	IEnumerator spawnEnemies ()
	{
		int i = 1;
		while (i <= nrEnemies) {
			int camPosXWidth = (int)(Camera.main.transform.position.x + Camera.main.rect.width);

			if (camPosXWidth % spawnRate == 0) {
				Enemy e = Instantiate (enemyObject, new Vector3 (camPosXWidth, Random.Range (CameraController.verticalBounds [1], 
				                                                                   CameraController.verticalBounds [0]), 1), Quaternion.identity) as Enemy; 
				i++;
			}
			yield return new WaitForSeconds (1);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
