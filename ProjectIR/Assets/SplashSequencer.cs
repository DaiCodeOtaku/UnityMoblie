using UnityEngine;
using System.Collections;
using System.Timers;

[RequireComponent(typeof(Canvas))]

public class SplashSequencer : MonoBehaviour {
	
	public static float sfxVol = 1.0f;
	public static float sfxUI = 10.0f;
	public static int ctrlScheme = 1;
	
	public Canvas canBrokenEarth;
	public Canvas canMenu;
	public Canvas canOptions;

	// Use this for initialization
	void Start () {
		canBrokenEarth.enabled = true;
		canMenu.enabled = false;
		canOptions.enabled = false;

	}

	// Update is called once per frame
	void Update () {
		if (SplashTimer.splashCount == 1){
			SplashTimer.splashInterval.Stop();
			SplashTimer.splashCount++;
			canBrokenEarth.enabled = false;
			canMenu.enabled = true;
		}
	}

	public void pressOptions(){
		Debug.Log ("options");
		canMenu.enabled = false;
		canOptions.enabled = true;
	}
	
	public void pressStart(){
		Debug.Log ("start");
		Application.LoadLevel ("Main");
	}
	
	public void pressBack(){
		Debug.Log ("back");
		canMenu.enabled = true;
		canOptions.enabled = false;
	}
	
}
