using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour
{

	public AudioClip[] jumpSounds;
	public AudioClip powerupSound;
    public AudioClip[] dieSounds;

	public float moveSpeed = 10.0f;
	public float moveSpeedJumpScale = 0.5f;
	public float jumpScale = 2.0f;
	public float jumpSpeed = 5.0f;

	private bool isInJump = false;

	private Animator animator;
	private AudioSource audioSource;

	private KeyCode attackKeyCode = KeyCode.LeftShift;

	// Use this for initialization
	void Start ()
	{
		animator = this.GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();

		Game.Instance.GameRestart += OnGameRestart;
	}

	void OnDestroy ()
	{
		if (Game.Instance != null)
			Game.Instance.GameRestart -= OnGameRestart;
	}
    
	// Update is called once per frame
	void Update ()
	{
		if (!Game.Instance.IsGameOver || Game.Instance.IsPaused) {
			DoMove ();
			DoJump ();
		}
	}

	void OnGameRestart ()
	{
		transform.position = Vector3.zero;
	}

	void DoMove ()
	{
		Vector3 direction = new Vector3 ();
         
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			direction.y = 1f;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			direction.y = -1f;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			direction.x = -1;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction.x = 1;
		}

		if (direction.x != 0f || direction.y != 0f) {
			//start walking
			animator.SetInteger ("walk", 1);

			transform.position += direction.normalized * Time.deltaTime * moveSpeed;

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			transform.localRotation = Quaternion.AngleAxis (angle + 180f, Vector3.forward);

		} else {
			//stop walking
			animator.SetInteger ("walk", 0);
		}

		//attack
		if (Input.GetKeyDown (attackKeyCode)) {
			animator.SetInteger ("attack", 1);
		}
		//stop attack
		if (Input.GetKeyUp (attackKeyCode)) {
			animator.SetInteger ("attack", 0);
		}
	}

	void DoJump ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !isInJump && !Game.Instance.PowerupManager.IsPowerupActive (PowerupManager.E_PowerupType.PowerDownJump)) {
			audioSource.PlayOneShot (jumpSounds [Random.Range (0, jumpSounds.Length)]);
			StartCoroutine (_Jump ());
		}
	}

	IEnumerator _Jump ()
	{
		isInJump = true;
		GetComponent<Collider2D> ().enabled = false;

		float originalScale = transform.localScale.x;
		float originalScaleZ = transform.localScale.z;
		float currentJumpScale = originalScale;
        
		float originalMoveSpeed = moveSpeed;
		moveSpeed *= moveSpeedJumpScale;

		// jump up
		while (currentJumpScale < jumpScale) {
			currentJumpScale += Time.deltaTime * jumpSpeed;
			currentJumpScale = Mathf.Clamp (currentJumpScale, originalScale, jumpScale);
            
			transform.localScale = new Vector3 (currentJumpScale, currentJumpScale, originalScaleZ);
			yield return new WaitForEndOfFrame ();
		}

		// jump back
		while (currentJumpScale > originalScale) {
			currentJumpScale -= Time.deltaTime * jumpSpeed;
			currentJumpScale = Mathf.Clamp (currentJumpScale, originalScale, jumpScale);

			transform.localScale = new Vector3 (currentJumpScale, currentJumpScale, originalScaleZ);
			yield return new WaitForEndOfFrame ();
		}

		GetComponent<Collider2D> ().enabled = true;
		isInJump = false;
		moveSpeed = originalMoveSpeed;
	}

	public void die ()
	{
        audioSource.PlayOneShot(dieSounds [Random.Range (0, dieSounds.Length)]);

		Debug.Log ("AVATAR MRTVY");
		//TODO animace
		Game.Instance.IsGameOver = true;
	}

	void killEnemy (Enemy enemy)
	{
		enemy.die ();
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (Game.Instance.PowerupManager.ProcessCollision (collision.gameObject))
			audioSource.PlayOneShot (powerupSound);

		if (collision.gameObject.name.StartsWith ("Enemy")) {
			if (Input.GetKey (attackKeyCode)) {
				killEnemy (collision.gameObject.GetComponent<Enemy> ());
			}					
		}
	}

	void OnBecameInvisible ()
	{
		die();
	}
}
