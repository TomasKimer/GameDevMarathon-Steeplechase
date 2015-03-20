using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public float moveSpeed = 10.0f;
    public float jumpScale = 2.0f;
    public float jumpSpeed = 5.0f;

    private bool isInJump = false;

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
        DoMove();
        DoJump();
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
            transform.position += direction.normalized * Time.deltaTime * moveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        }
    }

    void DoJump() {
        if (Input.GetKeyDown(KeyCode.Space) && !isInJump) {
           StartCoroutine(_Jump());
        }
    }

    IEnumerator _Jump() {
        isInJump = true;
        GetComponent<Collider2D>().enabled = false;

        float originalScale = transform.localScale.x;
        float originalScaleZ = transform.localScale.z;
        float currentJumpScale = originalScale;

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
    }

    void OnCollisionEnter2D(Collision2D collision) {
//      if (isInJump)
//          Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());        
        //Debug.Log("Collision enter: " + collision);
    }
}
