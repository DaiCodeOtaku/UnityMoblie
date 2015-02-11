using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Canvas))]

public class BtnScreen : MonoBehaviour {

	public Canvas canPause;
	public Canvas canDeath;

	// Use this for initialization
	void Start () {
		canPause.enabled = false;
		canDeath.enabled = false;

	}

	public void pressPlay(){
		Debug.Log ("play");
		canPause.enabled = false;
	}

	public void pressRetry(){
		Debug.Log ("retry");
		canDeath.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
