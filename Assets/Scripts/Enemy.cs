using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public GameObject avatar;
	public float moveSpeed = 1.0f;

	//private Transform playerPos;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (avatar.transform.position, new Vector3 (0, 0, 1));
		//TODO!!!!!!!!
		//transform.LookAt(new Vector3(a))

		transform.position += transform.forward * Time.deltaTime * moveSpeed;
	}
}
