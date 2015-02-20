using UnityEngine;
using System.Collections;

public class BtnOptions : MonoBehaviour {

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
		}
	}

	public void pressRaiseSfx(){
		if (SplashSequencer.sfxUI < 10){
			SplashSequencer.sfxUI++;
			SplashSequencer.sfxVol = SplashSequencer.sfxUI / 10;
			Debug.Log ("Volume: " + SplashSequencer.sfxVol);
		}
	}

	public void pressLowerControl(){
		if (SplashSequencer.ctrlScheme > 1){
			SplashSequencer.ctrlScheme--;
		}
	}
	
	public void pressRaiseControl(){
		if (SplashSequencer.ctrlScheme < 11){
			SplashSequencer.ctrlScheme++;
		}
	}

}
