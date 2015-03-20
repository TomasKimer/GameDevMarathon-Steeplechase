using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public float m_MoveSpeed = 10.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        Vector3 moveVec = new Vector3();
         
        if (Input.GetKey(KeyCode.W)) {
            moveVec.y = 1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveVec.y = -1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            moveVec.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            moveVec.x = 1;
        }

        Transform tr = GetComponent<Transform>();
        tr.Translate(moveVec * Time.deltaTime * m_MoveSpeed);

	}
}
