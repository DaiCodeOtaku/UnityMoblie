using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class BtnSfxTick : MonoBehaviour {

	public Sprite sfxOff;
	private Button button;

	// Use this for initialization
	void Start () {
		SplashSequencer.sfxToggle = 1;
		button = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		if (SplashSequencer.sfxToggle == 1){
			button.image.overrideSprite = sfxOff;
		} else {
			button.image.overrideSprite = null;
		}
	}

	public void pressSfxToggle(){
		if (SplashSequencer.sfxToggle == 1){
			SplashSequencer.sfxToggle = 0;
			Debug.Log("sfx disabled");
			// pass val to xml
			
		} else {
			SplashSequencer.sfxToggle = 1;
			Debug.Log("sfx enabled again");
		}
	}
}
