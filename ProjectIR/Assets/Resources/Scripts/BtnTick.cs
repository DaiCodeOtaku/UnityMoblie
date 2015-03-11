using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class BtnTick : MonoBehaviour {
	public static bool tiltAllow = true;
	public Sprite toggleOff;
	private Button button;

	// Use this for initialization
	void Start () {
		SplashSequencer.ctrlScheme = 0;
		button = GetComponent<Button>();
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
			// pass val to xml
			
		} else {
			tiltAllow = true;
			Debug.Log("now enabled again");
			if (SplashSequencer.ctrlScheme == 10){
				SplashSequencer.ctrlScheme = 8;
			}
		}
	}
}
