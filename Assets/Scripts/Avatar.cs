using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

	const	KeyCode		ATTACK_KEYCODE		= KeyCode.LeftShift;

	public	AudioClip[]	jumpSounds;
	public	AudioClip	powerupSound;
	public	AudioClip[]	dieSounds;

	public	float		moveSpeed			= 10.0f;
	public	float		moveSpeedJumpScale	= 0.5f;
	public	float		jumpScale			= 2.0f;
	public	float		jumpSpeed			= 5.0f;
	public	int			maxLives			= 3;

	private	int			m_Lives;
	private	bool		m_IsInJump			= false;
	private	Animator	m_Animator;
	private	AudioSource	m_AudioSource;
	private	Collider2D	m_Collider;

	void Awake() {
		m_Animator		= GetComponent<Animator>();
		m_AudioSource	= GetComponent<AudioSource>();
		m_Collider		= GetComponent<Collider2D>();
		m_Lives			= maxLives;
	}

	void Start() {
		Game.Instance.GameRestart += OnGameRestart;
	}

	void OnDestroy() {
		if (Game.Instance != null)
			Game.Instance.GameRestart -= OnGameRestart;
	}

	void Update() {
		if (!Game.Instance.IsGameOver || Game.Instance.IsPaused) {
			DoMove();
			DoJump();
		}
	}

	void OnGameRestart() {
		transform.position	= Vector3.zero;
		m_Lives				= maxLives;

		m_Animator.SetInteger("death", 0);
	}

	void DoMove() {
		Vector3 direction = new Vector3();

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			direction.y =  1f;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			direction.y = -1f;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			direction.x = -1f;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			direction.x =  1f;
		}

		if (direction.x != 0f || direction.y != 0f) {
			//start walking
			m_Animator.SetInteger("walk", 1);

			transform.position += direction.normalized * Time.deltaTime * moveSpeed;

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.localRotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);

		} else {
			//stop walking
			m_Animator.SetInteger("walk", 0);
		}

		//attack
		if (Input.GetKeyDown(ATTACK_KEYCODE)) {
			m_Animator.SetInteger("attack", 1);
		}
		//stop attack
		if (Input.GetKeyUp(ATTACK_KEYCODE)) {
			m_Animator.SetInteger("attack", 0);
		}
	}

	void DoJump () {
		if (Input.GetKeyDown(KeyCode.Space) && !m_IsInJump && !Game.Instance.PowerupManager.IsPowerupActive(PowerupManager.E_PowerupType.PowerDownJump)) {
			m_AudioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
			StartCoroutine( _Jump() );
		}
	}

	IEnumerator _Jump() {
		m_IsInJump				= true;
		m_Collider.enabled		= false;

		float originalScale		= transform.localScale.x;
		float originalScaleZ	= transform.localScale.z;
		float currentJumpScale	= originalScale;        
		float originalMoveSpeed	= moveSpeed;
		
		moveSpeed *= moveSpeedJumpScale;

		// jump up
		while (currentJumpScale < jumpScale) {
			currentJumpScale += Time.deltaTime * jumpSpeed;
			currentJumpScale  = Mathf.Clamp(currentJumpScale, originalScale, jumpScale);

			transform.localScale = new Vector3(currentJumpScale, currentJumpScale, originalScaleZ);
			yield return new WaitForEndOfFrame();
		}

		// jump back
		while (currentJumpScale > originalScale) {
			currentJumpScale -= Time.deltaTime * jumpSpeed;
			currentJumpScale  = Mathf.Clamp(currentJumpScale, originalScale, jumpScale);

			transform.localScale = new Vector3(currentJumpScale, currentJumpScale, originalScaleZ);
			yield return new WaitForEndOfFrame();
		}

		m_Collider.enabled	= true;
		m_IsInJump			= false;
		moveSpeed			= originalMoveSpeed;
	}

	public void die(bool outOfScreen) {
		m_AudioSource.PlayOneShot(dieSounds[Random.Range(0, dieSounds.Length)]);
		
		if (!outOfScreen) {
			m_Lives--;
			if (m_Lives <= 0) {
//				Debug.Log("AVATAR DIE");
				m_Animator.SetInteger("death", 1);
			}
		}

		Game.Instance.IsGameOver = true;
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (Game.Instance.PowerupManager.ProcessCollision(collision.gameObject))
			m_AudioSource.PlayOneShot(powerupSound);

		if (collision.gameObject.name.StartsWith("Enemy")) {
			if (Input.GetKey(ATTACK_KEYCODE)) {
				collision.gameObject.GetComponent<Enemy>().die();
			}
		}
	}

	void OnBecameInvisible () {
		die(true);	//true -- avatar out of the screen
	}
}
