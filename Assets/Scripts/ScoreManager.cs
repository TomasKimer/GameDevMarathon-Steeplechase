using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;        // The player's score.

	Text text;                      // Reference to the Text component.


	// Use this for initialization
	void Awake () {
	
		// Set up the reference.
		text = GetComponent <Text> ();
		
		// Reset the score.
		score = 0;

	}
	
	// Update is called once per frame
	void Update () {

		score += 1;

		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = score.ToString();

	}
}
