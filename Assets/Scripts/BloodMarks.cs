using UnityEngine;
using System.Collections;

public class BloodMarks : MonoBehaviour {

	const int LAYER		= 9;

	public	GameObject	bloodHelp;
	public	GameObject	bloodStain;
	public	GameObject	bloodDie;

	public	int			numberOfOccurs	= 100;
	public	int			maxScale		= 10;

	void Start() {
		for (int i = 0; i <= numberOfOccurs; i++) {
			(Instantiate(bloodHelp , genPosition(), genRotation()) as GameObject).transform.localScale = genScale();
			(Instantiate(bloodDie  , genPosition(), genRotation()) as GameObject).transform.localScale = genScale();
			(Instantiate(bloodStain, genPosition(), genRotation()) as GameObject).transform.localScale = genScale();
		}
	}

	Vector3 genScale() {
		int scale = Random.Range(0, maxScale);
		
		return new Vector3(scale, scale, 1);
	}

	Vector3 genPosition() {
		float posX = Random.Range(CameraController.horizontalBounds[1], CameraController.horizontalBounds[0]);
		float posY = Random.Range(CameraController.verticalBounds  [1], CameraController.verticalBounds  [0]);
		
		return new Vector3(posX, posY, LAYER);
	}

	Quaternion genRotation() {
		return Quaternion.Euler(0, 0, Random.Range(0, 360));
	}
}
