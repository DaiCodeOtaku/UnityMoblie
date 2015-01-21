using UnityEngine;
using System.Collections;
	

public class StartGui : MonoBehaviour {
	public Texture btnTexture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	



	}

	void OnGUI() {
				if (!btnTexture) {
						Debug.LogError ("Please assign a texture on the inspector");
						return;
				}
				if (GUI.Button (new Rect (300, 500, 400, 200), btnTexture))
						Debug.Log ("Clicked the button with an image");
				if (GUI.Button (new Rect (300, 400, 100, 30), "Click"))
						Debug.Log ("Clicked the button with text");

		}

}

