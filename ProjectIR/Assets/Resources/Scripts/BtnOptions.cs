using UnityEngine;
using System.Collections;

public class BtnOptions : MonoBehaviour {
	public static XML xml = GameObject.FindObjectOfType<XML>();
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}


	public void pressLogo(){
		Debug.Log ("skip logo");
		SplashTimer.splashCount = 1;
	}

	public void pressLowerSfx(){
		if (SplashSequencer.sfxUI > 0){
			SplashSequencer.sfxUI--;
			SplashSequencer.sfxVol = SplashSequencer.sfxUI / 10;
			Debug.Log ("Volume: " + SplashSequencer.sfxVol);
			xml.MusicWrite(SplashSequencer.sfxVol);
		}
	}

	public void pressRaiseSfx(){
		if (SplashSequencer.sfxUI < 10){
			SplashSequencer.sfxUI++;
			SplashSequencer.sfxVol = SplashSequencer.sfxUI / 10;
			Debug.Log ("Volume: " + SplashSequencer.sfxVol);
			xml.MusicWrite(SplashSequencer.sfxVol);
		}
	}

	public void pressLowerControl(){
		if (SplashSequencer.ctrlScheme > 0){

			if (BtnTick.tiltAllow == false && (SplashSequencer.ctrlScheme == 5 || SplashSequencer.ctrlScheme == 10)){
				if (SplashSequencer.ctrlScheme == 10){
					SplashSequencer.ctrlScheme--;
				}
				SplashSequencer.ctrlScheme--;
			}

			SplashSequencer.ctrlScheme--;
			setControlUI();
		} else {
			if (BtnTick.tiltAllow == false){
				SplashSequencer.ctrlScheme = 8;
				setControlUI();
				SplashSequencer.ctrlScheme = 10;
			} else {
				SplashSequencer.ctrlScheme = 9;
				setControlUI();
			}

		}
	}
	
	public void pressRaiseControl(){
		if (SplashSequencer.ctrlScheme < 9){

			if (BtnTick.tiltAllow == false && (SplashSequencer.ctrlScheme == 3 || SplashSequencer.ctrlScheme == 8)){
				SplashSequencer.ctrlScheme++;
			}

			SplashSequencer.ctrlScheme++;
			setControlUI();
		} else {
			SplashSequencer.ctrlScheme = 0;
			setControlUI();
		}
	}

	// changes the option screen's preferred control to a string and passes the value into xml
	public static void setControlUI(){


		switch (SplashSequencer.ctrlScheme) {
		case 0:
			setControlText("Move Scale");
			xml.UpdateControl(0);
		break;

		case 1:
			setControlText("Arrows");
			xml.UpdateControl(1);
		break;

		case 2:
			setControlText("Accel");
			xml.UpdateControl(2);
		break;

		case 3:
			setControlText("Stop on a Dime");
			xml.UpdateControl(3);
		break;

		case 4:
			setControlText("Tilt");
			xml.UpdateControl(9);
		break;

		case 5:
			setControlText("Move Scale Inverted");
			xml.UpdateControl(4);
		break;
			
		case 6:
			setControlText("Arrows Inverted");
			xml.UpdateControl(5);
		break;
			
		case 7:
			setControlText("Accel Inverted");
			xml.UpdateControl(6);
		break;
			
		case 8:
			setControlText("Stop on a Dime Inverted");
			xml.UpdateControl(7);
		break;
			
		case 9:
			setControlText("Tilt Inverted");
			xml.UpdateControl(8);
		break;
		}


	}

	public static void setControlText(string name){
		valControlScheme.txtVal = name;
	}


}
