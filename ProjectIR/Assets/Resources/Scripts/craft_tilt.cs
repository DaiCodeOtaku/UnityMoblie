using UnityEngine;
using System.Collections;

public class craft_tilt : MonoBehaviour {
	public float tilt_facter;

	private Vector3 grepp_pos;

	// Use this for initialization
	void Start ()
		{ grepp_pos = this.transform.position; }
	
	// Update is called once per frame
	void Update () {
		float pos_dif = grepp_pos.x - this.transform.position.x;
		print(pos_dif * tilt_facter);

		this.transform.rotation = Quaternion.AngleAxis(pos_dif * tilt_facter, Vector3.up);

		grepp_pos = this.transform.position;
	}
}