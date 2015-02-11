using UnityEngine;
using System.Collections;

public class BtnOptions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pressLowerSfx(){
		if (SplashSequencer.sfxVol > 0){
			SplashSequencer.sfxVol--;
		}
	}

	public void pressRaiseSfx(){
		if (SplashSequencer.sfxVol < 10){
			SplashSequencer.sfxVol++;
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
