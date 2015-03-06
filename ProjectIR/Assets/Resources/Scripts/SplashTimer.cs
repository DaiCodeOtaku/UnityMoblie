using UnityEngine;
using System.Collections;
using System.Timers;

public class SplashTimer : MonoBehaviour {

	public static Timer splashInterval = new Timer();
	public static int splashCount = 0;

	// Use this for initialization
	void Start () {
		splashInterval.Elapsed += new ElapsedEventHandler (splashSequence);
		splashInterval.Interval = 3000;
		splashInterval.Start ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void splashSequence(object source, ElapsedEventArgs e){
		splashCount = 1;
	}
}
