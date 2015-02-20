using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class valControlScheme : MonoBehaviour {
	public Text txt;
	public string txtVal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		txtVal = SplashSequencer.ctrlScheme.ToString ();
		txt.text = txtVal;
	}
}
