using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public float score;
	public bool GameOn;

	// Use this for initialization
	void Start () {
		score = 0;
		GameOn = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameOn){
			score += Time.deltaTime;
		}
	}
}
