using UnityEngine;
using System.Collections;

public class blade_spin : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0.0f, -16.0f, 0.0f));
	}
}
