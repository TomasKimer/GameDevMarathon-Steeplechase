using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public float moveSpeed = 10.0f;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
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
            Transform tr = GetComponent<Transform>();
            tr.position += direction.normalized * Time.deltaTime * moveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            tr.localRotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
    }


    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision enter: " + collision);
    
    }
}
