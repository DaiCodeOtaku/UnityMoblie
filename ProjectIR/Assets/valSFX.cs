using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class valSFX : MonoBehaviour {
	public Text txt;
	public string txtVal;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (SplashSequencer.sfxUI == 0){
			txt.text = "Off";;
		} else {
			txtVal = SplashSequencer.sfxUI.ToString ();
			txt.text = txtVal;
		}

	}
}
