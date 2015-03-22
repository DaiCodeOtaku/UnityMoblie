using UnityEngine;
using System.Collections;
using System.Timers;

[RequireComponent(typeof(Canvas))]

public class SplashSequencer : MonoBehaviour {
	
	public static int sfxToggle;
	public static int ctrlScheme;
	
	public Canvas canBrokenEarth;
	public Canvas canMenu;
	public Canvas canOptions;
	public Canvas canTut;

	GameObject LOGOBK;
	GameObject LOGO;
	
	// Use this for initialization
	void Start () {
		canBrokenEarth.enabled = true;
		canMenu.enabled = false;
		canOptions.enabled = false;
		canTut.enabled = false;
		LOGOBK = GameObject.Find("LOGOBK");
		LOGO = GameObject.Find ("LOGO");
	}
	
	// Update is called once per frame
	void Update () {
		if (SplashTimer.splashCount == 1){
			SplashTimer.splashInterval.Stop();
			SplashTimer.splashCount++;
			canBrokenEarth.enabled = false;
			LOGOBK.SetActive(false);
			canMenu.enabled = true;
		}
	}
	
	public void pressOptions(){
		Debug.Log ("options");
		canMenu.enabled = false;
		canOptions.enabled = true;
		LOGO.SetActive(false);
		GameObject.FindObjectOfType<BtnOptions>().refresh();
		GameObject.FindObjectOfType<BtnOptions>().setControlUI();
		//BtnOptions.setControlUI();
	}
	
	public void pressTut(){
		Debug.Log ("tut");
		canTut.enabled = true;
		canMenu.enabled = false;
	}
	
	public void pressStart(){
		Debug.Log ("start");
		Application.LoadLevel ("Main");
	}
	
	public void pressBack(){
		Debug.Log ("back");
		LOGOBK.SetActive(false);
		LOGO.SetActive (true);
		canMenu.enabled = true;
		canOptions.enabled = false;
		canTut.enabled = false;
	}
	
	public void pressExit(){
		Application.Quit ();
	}
	
}
