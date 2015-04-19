using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public	GameObject		followTarget;
	public	float			movementSpeed		= 1f;

	private	bool			killed = false;
	private	Collision2D		currCollision;
	private	Animator		animator;
	private	int[]			deathAnimations		= new int[] {1, 2};
	
	void Awake () {
		animator		= GetComponent<Animator>();
		followTarget	= GameObject.Find("/Avatar");
	}
	
	void Update () {
		if (!Game.Instance.IsGameOver && !Game.Instance.IsPaused && followTarget != null && !killed) {
			Vector3 nTransform = followTarget.transform.position - transform.position;
			nTransform.Normalize();

			transform.Translate(nTransform * Time.deltaTime * movementSpeed, Space.World); 
		}
	}

	public void die() {
		killed = true;
//		Debug.Log("ENEMY DIE");
		animator.SetInteger("death", deathAnimations[Random.Range(0, deathAnimations.Length)]);
	}

	IEnumerator killAvatar() {
		yield return new WaitForSeconds (0.5f);
		
		if (!killed && !Game.Instance.IsEasyMode) {
			Avatar avatar = currCollision.gameObject.GetComponent<Avatar>();
			avatar.die(false);	//false -- avatar dies on screen, not out of the screen
		}
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.name == "Avatar") {
			currCollision = collision;
			StartCoroutine("killAvatar");
		}
	}
}
