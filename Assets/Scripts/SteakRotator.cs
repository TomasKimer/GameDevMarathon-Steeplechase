using UnityEngine;
using System.Collections;

public class SteakRotator : MonoBehaviour {

	public	int		direction	= 1;	
	public	float	speed		= 100f;

	void Update () {		
		float rotZ		= direction * speed * Time.deltaTime;
		float scaleXY	= Mathf.PingPong(Time.time * 0.5f, 0.3f) + 0.6f;
		
		transform.Rotate(new Vector3(0, 0, rotZ));
		transform.localScale = new Vector3(scaleXY, scaleXY, 1.0f);
	}
}
