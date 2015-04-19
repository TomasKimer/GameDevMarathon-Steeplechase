using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	const float	GROUND_TILE_SIZE	= 100;
	const int	GROUND_AREA			= 2;
	const float	WALL_RANDOMNESS		= 2f;

	// ground sprites
	public	GameObject			groundPrefab;

	// wall prefabs
	public	GameObject			wallShortPrefab;
	public	GameObject			wallLongPrefab;
	public	GameObject			wallUPrefab;
	public	GameObject			wallLPrefab;
	
	[Range(0.0f, 1.0f)]
	public	float				tintInterval		= 0.12f;

	private	SpriteRenderer[]	m_groundSprites;


	void Start() {
		CreateFloor();
		CreateWalls();
		CreatePowerUps();

		m_groundSprites = GetComponentsInChildren<SpriteRenderer>();

		StartCoroutine ( _TintRepeated() );
	}

	// puts all the floor tiles in the scene
	void CreateFloor() {
		int areaOffset = -55;

		for (int j = -GROUND_AREA - 1; j < GROUND_AREA + 1; ++j) {
			GameObject ground = Instantiate(groundPrefab, new Vector3(j * GROUND_TILE_SIZE, GROUND_TILE_SIZE + areaOffset, 10f), Quaternion.identity) as GameObject;
			ground.transform.parent = transform;
		}
	}

	// generates walls on the floor area
	void CreateWalls() {
		int screenOffset = 17;

		for (int j = -GROUND_AREA - 1; j < GROUND_AREA + 1; ++j) {
			for (int i = 0; i < 1; ++i) {
				// inside the floor sprite

				float xOffset	= 0;
				float yOffset	= 0;

				float xcoord	= 0;
				float ycoord	= 0; 

				for (int k = 0; k <= 100; k += 10) {
					for (int l = 0; l <= 40; l += 10) {

						xOffset = Random.Range(-WALL_RANDOMNESS, WALL_RANDOMNESS);
						yOffset = Random.Range(-WALL_RANDOMNESS, WALL_RANDOMNESS);

						xcoord = j * GROUND_TILE_SIZE + k + xOffset;
						ycoord = i * GROUND_TILE_SIZE - l + yOffset + screenOffset;

						if (Mathf.Abs(xcoord) < 10 && Mathf.Abs(ycoord) < 10) {
//							Debug.Log ("Negeneruji!!!!!!" + "x: " + xcoord + "y: " + ycoord);
							continue;
						}

						int			select		= Random.Range (0, 10);
						GameObject	objectType	= wallShortPrefab;	// short wall

						if (select < 3) {
							objectType = wallShortPrefab;			// long wall
						} else if (select < 5) {
							objectType = wallUPrefab;				// U
						} else if (select < 8) {
							objectType = wallLPrefab;				// L
						}

						// generate wall
						GameObject wall = Instantiate (objectType, new Vector3(xcoord, ycoord, 8), Quaternion.Euler(0, 0, (Random.Range(0, 359)))) as GameObject;
						//wall.transform.parent = transform;
						wall.SetActive(true);
					}
				}
			}
		}
	}

	void CreatePowerUps() {
		int screenOffset = 17;
		
		for (int j = -GROUND_AREA - 1; j < GROUND_AREA + 1; ++j) {
			for (int i = 0; i < 1; ++i) {
				// inside the floor sprite

				float xOffset	= 0;
				float yOffset	= 0;
				
				float xcoord	= 0;
				float ycoord	= 0; 
				
				for (int k = 0; k <= 100; k += 15) {
					for (int l = 0; l <= 40; l += 15) {
						
						xOffset = Random.Range(-6f, 6f);
						yOffset = Random.Range(-6f, 6f);
						
						xcoord = j * GROUND_TILE_SIZE + k + xOffset;
						ycoord = i * GROUND_TILE_SIZE - l + yOffset + screenOffset;
						
						if (Mathf.Abs(xcoord) < 5 && Mathf.Abs(ycoord) < 5) {
//							Debug.Log ("Negeneruji!!!!!!" + "x: " + xcoord + "y: " + ycoord);
							continue;
						}

						// generate powerup
						PowerupManager.E_PowerupType powerupType = (Random.Range(0, 10) < 7) ? PowerupManager.E_PowerupType.PowerUpCamera
						                                                                     : PowerupManager.E_PowerupType.PowerDownJump;
						Game.Instance.PowerupManager.GeneratePowerup(powerupType, new Vector3(xcoord, ycoord, 5), Quaternion.identity);
					}
				}
			}
		}
	}

//	void createWallAtXY(int x, int y, int rotMin, int rotMax) {
//		Vector3		pos		= new Vector3(x, Random.Range(-55, -10), 0);
//		Quaternion	rot		= Quaternion.Euler(0, 0, Random.Range(rotMin, rotMax));
//		GameObject	wall	= Instantiate(wallLongPrefab, pos, rot) as GameObject;
//
//		//wall.transform.parent = transform;
//		wall.SetActive(true);
//	}

	void SetTint(Color color) {
		foreach (SpriteRenderer sr in m_groundSprites) {
			sr.color = color;
		}
	}

	IEnumerator _TintRepeated() {
		while (true) {
			if (Game.Instance.PowerupManager.IsPowerupActive(PowerupManager.E_PowerupType.PowerUpCamera)) {
				float c = Random.Range(0.2f, 1f);
				SetTint(new Color(c, c, c));
			} else {
				SetTint(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
			}
			yield return new WaitForSeconds (tintInterval);
		}
	}
}
