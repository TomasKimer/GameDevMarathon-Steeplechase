using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public float moveSpeed = 10.0f;
    public float moveSpeedJumpScale = 0.5f;
    public float jumpScale = 2.0f;
    public float jumpSpeed = 5.0f;

    private bool isInJump = false;

	private Animator animator;

    // Use this for initialization
    void Start () {
		animator = this.GetComponent<Animator>();
        Game.Instance.GameRestart += OnGameRestart;
    }

    void OnDestroy() {
        if (Game.Instance != null)
            Game.Instance.GameRestart -= OnGameRestart;
    }
    
    // Update is called once per frame
    void Update () {
        if (!Game.Instance.IsGameOver) {
            DoMove();
            DoJump();
        }
    }

    void OnGameRestart() {
        transform.position = Vector3.zero;
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
            direction.x = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction.x =  1;
        }

        if (direction.x != 0f || direction.y != 0f) {
			//start walking
			animator.SetInteger("walk", 1);

            transform.position += direction.normalized * Time.deltaTime * moveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);

        } else {
			//stop walking
			animator.SetInteger("walk", 0);
		}

		//attack
		KeyCode attackKeyCode = KeyCode.LeftShift;
		if(Input.GetKeyDown(attackKeyCode)) {
			animator.SetInteger("attack", 1);
		}
		//stop attack
		if(Input.GetKeyUp(attackKeyCode)) {
			animator.SetInteger("attack", 0);
		}
    }

    void DoJump() {
        if (Input.GetKeyDown(KeyCode.Space) && !isInJump && !Game.Instance.PowerupManager.IsPowerupActive(PowerupManager.E_PowerupType.PowerDownJump)) {
           StartCoroutine(_Jump());
        }
    }

    IEnumerator _Jump() {
        isInJump = true;
        GetComponent<Collider2D>().enabled = false;

        float originalScale = transform.localScale.x;
        float originalScaleZ = transform.localScale.z;
        float currentJumpScale = originalScale;
        
        float originalMoveSpeed = moveSpeed;
        moveSpeed *= moveSpeedJumpScale;

        // jump up
        while (currentJumpScale < jumpScale) {
            currentJumpScale += Time.deltaTime * jumpSpeed;
            currentJumpScale = Mathf.Clamp(currentJumpScale, originalScale, jumpScale);
            
            transform.localScale = new Vector3(currentJumpScale, currentJumpScale, originalScaleZ);
            yield return new WaitForEndOfFrame();
        }

        // jump back
        while (currentJumpScale > originalScale) {
            currentJumpScale -= Time.deltaTime * jumpSpeed;
            currentJumpScale = Mathf.Clamp(currentJumpScale, originalScale, jumpScale);

            transform.localScale = new Vector3(currentJumpScale, currentJumpScale, originalScaleZ);
            yield return new WaitForEndOfFrame();
        }

        GetComponent<Collider2D>().enabled = true;
        isInJump = false;
        moveSpeed = originalMoveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Game.Instance.PowerupManager.ProcessCollision(collision.gameObject);
    }

    void OnBecameInvisible() {
        Game.Instance.IsGameOver = true;
    }
}
