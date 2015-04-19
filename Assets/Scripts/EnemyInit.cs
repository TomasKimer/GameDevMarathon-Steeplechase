using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInit : MonoBehaviour {

	public	GameObject	enemyObject;
	public	int			nrEnemies		= 50;
	public	int			spawnDistance	= 1;
	public	int			spawnRate		= 2;

	void Start() {
		Game.Instance.GameRestart += OnGameRestart;
		
		StartCoroutine ("spawnEnemies");
	}

	void OnGameRestart() {
		Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		
		foreach (Enemy enemy in enemies) {
			Destroy(enemy.gameObject);
		}

		StopCoroutine ("spawnEnemies");
		StartCoroutine("spawnEnemies");
	}
	
	IEnumerator spawnEnemies() {
		int i = 1;
		
		while (i <= nrEnemies) {
			int camPosXWidth = (int)(Camera.main.transform.position.x + Camera.main.rect.width);

			if (camPosXWidth % spawnRate == 0) {
				float yPos = Random.Range(CameraController.verticalBounds[1], CameraController.verticalBounds[0]);

				Instantiate(enemyObject, new Vector3(camPosXWidth, yPos, 1.0f), Quaternion.identity); 
				
				i++;
			}
			yield return new WaitForSeconds(1);
		}
	}
}
