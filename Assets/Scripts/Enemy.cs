using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
//	[SerializeField]
	public GameObject  followTarget;

	//public GameObject FollowTarget{ get { return followTarget; } set { followTarget = value; } }
	public float movementSpeed = 1f;

	private bool killed = false;
	private Collision2D currCollision;

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

//	void killAvatar (Avatar avatar)
//	{
//		avatar.die ();
//	}

	public void die ()
	{
		killed = true;
		Debug.Log ("ENEMY MRTVY");
	}

	IEnumerator killAvatar ()
	{
		yield return new WaitForSeconds (5f);
		if (!killed) {
			//killAvatar (currCollision.gameObject.GetComponent<Avatar> ());
			Avatar avatar = currCollision.gameObject.GetComponent<Avatar> ();
			avatar.die ();
		}
		yield break;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.name == "Avatar") {
			currCollision = collision;
			StartCoroutine ("killAvatar");
//			new WaitForSeconds (10f);
//			if (!killed) {
//				killAvatar (collision.gameObject.GetComponent<Avatar> ());
//			}			
		}

	}

}
