using UnityEngine;
using System.Collections;
using System.Timers;

[RequireComponent(typeof(Canvas))]

public class SplashSequencer : MonoBehaviour {

	public Timer splashInterval = new Timer();
	public int splashCount = 0;

	public Canvas canUnity;
	public Canvas canBrokenEarth;
	public Canvas canMenu;

	// Use this for initialization
	void Start () {
		splashInterval.Elapsed += new ElapsedEventHandler (splashSequence);
		splashInterval.Interval = 3000;
		splashInterval.Start ();

		canBrokenEarth.enabled = false;
		canMenu.enabled = false;
		
	}

	public void splashSequence(object source, ElapsedEventArgs e){
		splashCount++;

	}

	public void pressStart(){
		Debug.Log ("start");
		Application.LoadLevel ("Main");
	}

	// Update is called once per frame
	void Update () {
		if (splashCount < 3) {

			canUnity.enabled = false;
			canBrokenEarth.enabled = false;
			canMenu.enabled = false;
		}


		if (splashCount == 0) {
			canUnity.enabled = true;
		} else if (splashCount == 1){
			canBrokenEarth.enabled = true;
		} else if (splashCount == 2){
			canMenu.enabled = true;
			splashInterval.Stop();
			splashCount++;
		} 
	}
}
