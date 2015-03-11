using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class BtnSfxTick : MonoBehaviour {
	public XML xml;
	public Sprite sfxOff;
	public Button button;

	// Use this for initialization
	void Start () {
		//xml = GameObject.FindObjectOfType<XML>();
		SplashSequencer.sfxToggle = (int)xml.MusicRead();
		//button = GetComponent<Button>();
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
			xml.MusicWrite(0);
			
		} else {
			SplashSequencer.sfxToggle = 1;
			Debug.Log("sfx enabled again");
			xml.MusicWrite(1);
		}
	}
}
