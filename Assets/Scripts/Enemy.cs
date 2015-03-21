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
	private Animator animator;
	private int[] deathAnimations = new int[] {1, 2};
	
	void Awake ()
	{
		//!!!!set target to follow
		followTarget = GameObject.Find ("/Avatar");
	}

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Game.Instance.IsGameOver && !Game.Instance.IsPaused && followTarget != null && !killed) {
			Vector3 nTransform = followTarget.transform.position - transform.position;
		
			//make sure its of magnitude 1.		
			nTransform.Normalize ();

			transform.Translate (nTransform * Time.deltaTime * movementSpeed, Space.World); 
		}
	}

	public void die ()
	{
		killed = true;
		Debug.Log ("ENEMY MRTVY");
		animator.SetInteger ("death", deathAnimations [Random.Range (0, deathAnimations.Length)]);
	}

	IEnumerator killAvatar ()
	{
		yield return new WaitForSeconds (2f);
		if (!killed && !Game.Instance.IsEasyMode) {
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
