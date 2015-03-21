using UnityEngine;
using System.Collections;

public class BloodMarks : MonoBehaviour
{

	public GameObject bloodHelp;
	public GameObject bloodStain;
	public GameObject bloodDie;
	public int numberOfOccurs = 100;
	public int maxScale = 10;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i <= numberOfOccurs; i++) {
			(Instantiate (bloodHelp, genPosition (), genRotation ()) as GameObject).transform.localScale = genScale ();
			(Instantiate (bloodDie, genPosition (), genRotation ()) as GameObject).transform.localScale = genScale ();
			(Instantiate (bloodStain, genPosition (), genRotation ()) as GameObject).transform.localScale = genScale ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

	Vector3 genScale ()
	{
		int scale = Random.Range (0, maxScale);
		return new Vector3 (scale, scale, 1);
	}

	Vector3 genPosition ()
	{
		float posX = Random.Range (CameraController.horizontalBounds [1], CameraController.horizontalBounds [0]);
		float posY = Random.Range (CameraController.verticalBounds [1], CameraController.verticalBounds [0]);
		return new Vector3 (posX, posY, 9);
	}

	Quaternion genRotation ()
	{
		return Quaternion.Euler (0, 0, Random.Range (0, 360));
	}
}
