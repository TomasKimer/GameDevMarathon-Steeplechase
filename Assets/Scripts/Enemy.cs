using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
//	[SerializeField]
	public GameObject  followTarget;

	//public GameObject FollowTarget{ get { return followTarget; } set { followTarget = value; } }
	public float movementSpeed = 1f;

	//private Transform playerPos;

	void Awake ()
	{
		//!!!!set target to follow
		followTarget = GameObject.Find ("/Avatar");
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Game.Instance.IsGameOver && !Game.Instance.IsPaused && followTarget != null) {
			Vector3 nTransform = followTarget.transform.position - transform.position;
		
			//make sure its of magnitude 1.		
			nTransform.Normalize ();

			transform.Translate (nTransform * Time.deltaTime * movementSpeed, Space.World); 
		}
	}
}
