using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class BtnTick : MonoBehaviour {
	public static bool tiltAllow = true;
	public  XML xml;
	public Sprite toggleOff;
	private Button button;

	// Use this for initialization
	void Start () {
		SplashSequencer.ctrlScheme = 0;
		SplashSequencer.sfxUI = 10.0f;
		SplashSequencer.sfxVol = 1.0f;
		button = GetComponent<Button>();
		xml = GameObject.FindObjectOfType<XML>();
	}
	
	// Update is called once per frame
	void Update () {
		if (tiltAllow == false){
			button.image.overrideSprite = toggleOff;
		} else {
			button.image.overrideSprite = null;
		}
	}

	public void pressTiltAllow(){
		if (tiltAllow == true){
			tiltAllow = false;
			Debug.Log("now disabled");
			SplashSequencer.ctrlScheme = 0;
			BtnOptions.setControlText("Move Scale");

			
		} else {
			tiltAllow = true;
			Debug.Log("now enabled again");
			if (SplashSequencer.ctrlScheme == 10){
				SplashSequencer.ctrlScheme = 8;
			}
		}
		xml.TilteEnabledWrite(tiltAllow); 
	}
}
