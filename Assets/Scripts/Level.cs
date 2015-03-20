﻿using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	private const float groundTileSize = 100;
	private const int groundArea = 2;

	// ground sprites
	public GameObject groundPrefab;

	// wall prefabs
	public GameObject wallShortPrefab;
	public GameObject wallLongPrefab;
	public GameObject wallUPrefab;
	public GameObject wallLPrefab;

	
	[Range(0.0f, 1.0f)]
    public float tintInterval = 0.12f;

    private SpriteRenderer[] m_groundSprites;

    void Awake() {

    }

	// Use this for initialization
	void Start () {

		CreateFloor ();
		CreateWalls ();

        //Game.Instance.PowerupManager.GeneratePowerup(PowerupManager.E_PowerupType.PowerDownJump, Vector3.zero, Quaternion.identity);

		//wallPrefab.SetActive (false);

        m_groundSprites = GetComponentsInChildren<SpriteRenderer>();
        
        StartCoroutine( _TintRepeated() );
	}

	/**
	 * Puts all the floor tiles in the scene
	 */
	void CreateFloor(){
	
		for (int j = -groundArea-1; j < groundArea+1; j++) {
			for (int i=-groundArea+1; i<groundArea+1; i++) {
				GameObject ground = Instantiate (groundPrefab, new Vector3 (j * groundTileSize, i * groundTileSize, 10), 
				                                 Quaternion.identity) as GameObject;
                ground.transform.parent = transform;
			}
		}
	}
	/**
	 * Generates walls on the floor area
	 */
	void CreateWalls (){

		int screenOffset = 17;


		for (int j = -groundArea-1; j < groundArea+1; j++) {
			for (int i=0; i<1; i++) {
				// inside the floor sprite

				float xOffset = 0;
				float yOffset = 0;
				int select = 0;
				GameObject objectType;

				float xcoord = 0;
				float ycoord = 0; 

				for(int k=0; k<100; k+=8){
					for(int l=0; l<40; l+=8){

						xOffset = Random.Range(-3f, 3f);
						yOffset = Random.Range(-3f, 3f);

						select = Random.Range(0,10);
						if(select < 3){
							// long wall
							objectType = wallShortPrefab;

						} else if( select < 5) {
							// U
							objectType = wallUPrefab;
						} else if (select < 8) {
							// L
							objectType = wallLPrefab;
						} else {
							// short wall
							objectType = wallShortPrefab;
						}

						xcoord = j * groundTileSize + k + xOffset;
						ycoord = i * groundTileSize - l + yOffset + screenOffset;

						if(Mathf.Abs(xcoord) < 10 && Mathf.Abs(ycoord) < 10){
							Debug.Log("Negeneruji!!!!!!" + "x: " + xcoord + "y: " + ycoord);
							continue;
						}

						// generate wall
						GameObject wall = Instantiate (objectType, new Vector3 (xcoord, 
						                                                        ycoord, 10), 
						                               Quaternion.Euler (0, 0, (Random.Range(0, 359)))) as GameObject;
						//wall.transform.parent = transform;
					}
				}
			}
		}
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
	    
	}


	void createWallAtXY(int x, int y, int rotMin, int rotMax){

		GameObject wall = Instantiate(wallLongPrefab, new Vector3(x, Random.Range(-55, -10), 0), 
		            Quaternion.Euler (0, 0, (Random.Range(rotMin, rotMax)))) as GameObject;
        
        //wall.transform.parent = transform;
        wall.SetActive(true);
	}




    void SetTint(Color color) {
        foreach (SpriteRenderer sr in m_groundSprites) {
            sr.color = color;
        }
    }

    IEnumerator _TintRepeated() {
        for (;;) {
            if (Game.Instance.PowerupManager.HasCameraPowerUp) {
                float c = Random.Range(0f, 1f);
                SetTint(new Color(c, c, c));
            }
            else {
                SetTint(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            }
            yield return new WaitForSeconds(tintInterval);
        }
    }
}
