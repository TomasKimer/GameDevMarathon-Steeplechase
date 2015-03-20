using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	private const float groundTileSize = 100;

	public GameObject wallPrefab;
    public GameObject groundPrefab;
	public GameObject floorPrefab;


    [Range(0.0f, 1.0f)]
    public float tintInterval = 0.12f;

    private SpriteRenderer[] m_groundSprites;

    void Awake() {

    }

	// Use this for initialization
	void Start () {



		CreateFloor (4);


		// x = -5 .... x = 20
		// y =  -5 .... y = + 5

		for (int i=-5; i<=65; i+=3) {
			createWallAtX(i, 65, 115);
		}

		//InstantiateWall (-15, 15);

		wallPrefab.SetActive (false);

        m_groundSprites = GetComponentsInChildren<SpriteRenderer>();
        
        StartCoroutine( _TintRepeated() );
	}

	/**
	 * Puts all the floor tiles in the scene
	 */
	void CreateFloor(int size){
	
		for (int j = -size; j <= size; j++) {

			for (int i=-size; i<=size; i++) {
				GameObject ground = Instantiate (groundPrefab, new Vector3 (i * groundTileSize, j * groundTileSize, 10), 
				                                 Quaternion.identity) as GameObject;
                ground.transform.parent = transform;
			}

		}

	}


	// Update is called once per frame
	void Update () {
	    
	}

	void InstantiateWall(int minPos, int maxPos){
	
		Instantiate(wallPrefab, new Vector3(Random.Range(minPos, maxPos), Random.Range(minPos, maxPos), 0), 
		            Quaternion.Euler (0, 0, (Random.Range(0, 180))));
	}

	void createWallAtX(int x, int rotMin, int rotMax){

		GameObject wall = Instantiate(wallPrefab, new Vector3(x, Random.Range(-55, -10), 0), 
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
            SetTint(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            yield return new WaitForSeconds(tintInterval);
        }
    }
}
