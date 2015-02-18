using UnityEngine;
using System.Collections;
using System.Timers;

[RequireComponent(typeof(Canvas))]

public class SplashSequencer : MonoBehaviour {

	public Timer splashInterval = new Timer();
	public static int splashCount = 0;
	public static float sfxVol = 1.0f;
	public static float sfxUI = 10.0f;
	public static int ctrlScheme = 1;
	
	public Canvas canBrokenEarth;
	public Canvas canMenu;
	public Canvas canOptions;

	// Use this for initialization
	void Start () {
		splashInterval.Elapsed += new ElapsedEventHandler (splashSequence);
		splashInterval.Interval = 3000;
		splashInterval.Start ();

		canBrokenEarth.enabled = true;
		canMenu.enabled = false;
		canOptions.enabled = false;
		
	}

	public void splashSequence(object source, ElapsedEventArgs e){
		splashCount++;

	}

	// Update is called once per frame
	void Update () {
		if (splashCount < 2) {
			canBrokenEarth.enabled = false;
			canMenu.enabled = false;
		}


		if (splashCount == 0){
			canBrokenEarth.enabled = true;
		} else if (splashCount == 1){
			canMenu.enabled = true;
			splashInterval.Stop();
			splashCount++;
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
