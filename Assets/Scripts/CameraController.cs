using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public struct CameraSettings {
        public int movementSpeed;
	    public int rotationAngle;
	    public int rotationSpeed;
    }

    public CameraSettings[] m_CameraSettings;
	
	public static float[] verticalBounds = {10f, -10f};
	public static float[] horizontalBounds = {280f, -280f};

	private Quaternion originalRotation;
	private float offsetZ = -10f;
	private int cameraChangeSeconds = 2;
	private Vector3 movementTranslation;

    private int movementModifier = 1;

    private CameraSettings m_CurrentSettings {
        get {
            return m_CameraSettings[Mathf.Min(Game.Instance.CurrentLevel, m_CameraSettings.Length - 1)];
        }
    }

	private int[] randomLevelsX = new int[] {1, 2};
	private int[] randomLevelsY = new int[] {-1, 1};

	// Use this for initialization
	void Start ()
	{
		this.transform.position = new Vector3 (0, 0, offsetZ);
		transform.rotation = Quaternion.Euler (0, 0, 0);

		originalRotation = transform.rotation;

		StartCoroutine ("changeDirection");
		StartCoroutine ("shakeCam");

		Game.Instance.GameRestart += OnGameRestart;
	}

	void OnDestroy ()
	{
		if (Game.Instance != null)
			Game.Instance.GameRestart -= OnGameRestart;
	}

	void OnGameRestart ()
	{ 
		this.transform.position = new Vector3 (0, 0, offsetZ);
		transform.rotation = Quaternion.identity;
		originalRotation = transform.rotation;

		StopAllCoroutines ();
		StartCoroutine ("changeDirection");
		StartCoroutine ("shakeCam");
	}

	void OnEnable ()
	{
		//StartCoroutine("changeDirection");
		//StartCoroutine("shakeCam");
	}

	void OnDisable ()
	{
		//StopAllCoroutines();
	}
	
	IEnumerator changeDirection ()
	{
		for (;;) {
			movementTranslation.x = randomLevelsX [Random.Range (0, randomLevelsX.Length)];
			movementTranslation.y = randomLevelsY [Random.Range (0, randomLevelsY.Length)];
			//movementTranslation.z = ((int)Random.Range(0, 2));
			yield return new WaitForSeconds (cameraChangeSeconds); 
		}
	}
	
	IEnumerator shakeCam ()
	{
		//int rotation = rotationAngle;
		int negRotation = -1 * m_CurrentSettings.rotationAngle;
		float deltaRotation = 0;
		//int speed = rotationSpeed;
		bool forward = true;
		for (;;) {
			if (deltaRotation < m_CurrentSettings.rotationAngle && forward) {
				deltaRotation += m_CurrentSettings.rotationSpeed * Time.deltaTime;
			} else if (deltaRotation >= negRotation) {
				forward = false;
				deltaRotation -= m_CurrentSettings.rotationSpeed * Time.deltaTime;
			} else {
				forward = true;
				yield return null;
			}

			transform.rotation = Quaternion.Euler (originalRotation.eulerAngles.x, originalRotation.eulerAngles.y,
			                                      originalRotation.eulerAngles.z + deltaRotation);

			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		float newPositionY = transform.position.y + movementTranslation.y;

		if (newPositionY >= verticalBounds [0] || newPositionY <= verticalBounds [1]) {
			movementTranslation.y = 0;
		}

		if (transform.position.x >= horizontalBounds [0] || transform.position.x <= horizontalBounds [1]) {
			movementModifier *= -1;
		}

		transform.Translate (movementTranslation * Time.deltaTime * m_CurrentSettings.movementSpeed * movementModifier, Space.World); 
	}
}