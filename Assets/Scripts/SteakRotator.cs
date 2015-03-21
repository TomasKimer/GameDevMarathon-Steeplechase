using UnityEngine;
using System.Collections;

public class SteakRotator : MonoBehaviour {

	public int direction = 1;
	float speed = 100f;
	private Vector3 originalRotation;


	// Use this for initialization
	void Start () {
		originalRotation = transform.rotation.eulerAngles;
	
	}
	
	// Update is called once per frame
	void Update () {
		// The step size is equal to speed times frame time.
		float step = direction* speed * Time.deltaTime;
		
		// R
		transform.Rotate (new Vector3(0,0,step));
		float f = Mathf.PingPong (Time.time * 0.5f, 0.3f) + 0.6f;

		transform.localScale = new Vector3(f, f, 1);
	}
}
