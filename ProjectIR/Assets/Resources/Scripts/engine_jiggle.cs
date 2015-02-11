using UnityEngine;
using System.Collections;

public class engine_jiggle : MonoBehaviour {
	public float amp = 0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(
			Mathf.Sin(Random.value * Mathf.PI * 2) * amp,
			this.transform.position.y,
			this.transform.position.z);
	}
}
