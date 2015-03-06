using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sfxToggle : MonoBehaviour {

	public Text txt;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SplashSequencer.sfxToggle == 1){
			txt.text = "Sound On";
		} else {
			txt.text = "Sound Off";
		}
	}
}
