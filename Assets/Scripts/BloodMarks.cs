using UnityEngine;
using System.Collections;

public class BloodMarks : MonoBehaviour
{

	public GameObject bloodHelp;
	public GameObject bloodStain;
	public GameObject bloodDie;

	// Use this for initialization
	void Start ()
	{
		Instantiate (bloodDie, new Vector3 (), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}

//	Vector3 genPosition ()
//	{
//
//	}
}
