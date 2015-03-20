using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public float scoreUpdateInterval = 1f;

	public static int score;        // The player's score.

	Text text;                      // Reference to the Text component.

	// Use this for initialization
	void Awake () {
	
		// Set up the reference.
		text = GetComponent <Text> ();
		
		// Reset the score.
		score = 0;
        text.text = score.ToString();

        StartCoroutine( _UpdateScore() );
	}
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator _UpdateScore() {
        while (true) {
            yield return new WaitForSeconds(scoreUpdateInterval);
            
            if (!GameController.Instance.IsPaused)
                score++;
            text.text = score.ToString();
        }
    }
}
