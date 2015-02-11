using UnityEngine;
using System.Collections;

public class simpleSpin : MonoBehaviour {
	float rot = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (rot < 360) {
			rot += 1;
		}
		else {
			rot = 0;
		}

		this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
	}
}
