using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public int movementSpeed = 3;
	public int rotationAngle = 15;
	public int rotationSpeed = 3;
	public Vector2 verticalBounds = new Vector2(5, -5);

	private Quaternion originalRotation;
	private float offsetZ = -10f;
	private int cameraChangeSeconds = 2;
	private Vector3 movementTranslation;
	
	private int[] randomLevelsX = new int[] {1, 2};
	private int[] randomLevelsY = new int[] {-1, 1};

	// Use this for initialization
	void Start(){
		this.transform.position = new Vector3(0, 0, offsetZ);
		transform.rotation = Quaternion.Euler(0, 0, 0);

		originalRotation = transform.rotation;

		StartCoroutine("changeDirection");

		StartCoroutine("shakeCam");
	}
	
	IEnumerator changeDirection() {
		for(;;) {
			movementTranslation.x = randomLevelsX[Random.Range(0, randomLevelsX.Length)];
			movementTranslation.y = randomLevelsY[Random.Range(0, randomLevelsY.Length)];
			//movementTranslation.z = ((int)Random.Range(0, 2));
			yield return new WaitForSeconds(cameraChangeSeconds); 
		}
	}
	
	IEnumerator shakeCam() {
		//int rotation = rotationAngle;
		int negRotation = -1 * rotationAngle;
		int deltaRotation = 0;
		//int speed = rotationSpeed;
		bool forward = true;
		for(;;) {
			if(deltaRotation < rotationAngle && forward) {
				deltaRotation += rotationSpeed;
				transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y,
				                                     originalRotation.eulerAngles.z + deltaRotation);
			} else if(deltaRotation >= negRotation){
				forward = false;
				deltaRotation -= rotationSpeed;
				transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y,
				                                      originalRotation.eulerAngles.z + deltaRotation);
			} else {
				forward = true;
			}

			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update(){
		float newPos = movementTranslation.y + transform.position.y;

		if((newPos >= verticalBounds.x + 1) && (newPos <= verticalBounds.y + 1)) {
			movementTranslation.y = 0;
		}
		transform.Translate(movementTranslation * Time.deltaTime * movementSpeed);  		                                           
	}
}