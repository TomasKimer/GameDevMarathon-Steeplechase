using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public float moveSpeed = 10.0f;
    public float jumpScale = 2.0f;

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
            direction.y = 1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            direction.y = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction.x = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction.x = 1;
        }

        if (direction.x != 0f || direction.y != 0f) {
            transform.position += direction.normalized * Time.deltaTime * moveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
    }

    void DoJump() {
        if (Input.GetKeyDown(KeyCode.Space) && !isInJump) {
           StartCoroutine(_Jump());//transform.localScale = new Vector3(jumpScale, jumpScale, 1.0f);
        }
    }

    IEnumerator _Jump() {
        isInJump = true;

        float scale = transform.localScale.x;
        while (transform.localScale.x < jumpScale) {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * 2.0f, transform.localScale.x + Time.deltaTime * 2.0f, 1.0f);
            yield return new WaitForEndOfFrame();
        }

        while (transform.localScale.x > 1.0f) {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * 2.0f, transform.localScale.x - Time.deltaTime * 2.0f, 1.0f);
            yield return new WaitForEndOfFrame();
        }

        isInJump = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("Collision enter: " + collision);
    }
}
