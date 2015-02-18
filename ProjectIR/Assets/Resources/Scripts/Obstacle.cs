using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	float speed;

	// Use this for initialization
	void Start () {
		speed = 5.5f;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(speed * Vector3.down * Time.deltaTime);
		if(this.transform.position.y < -5){
			Destroy(this.gameObject);
			Destroy(this);
		}
	}

	void OnCollisionEnter() {
		//ObstacleController OC = (ObstacleController)GameObject.FindObjectOfType<ObstacleController>();

		Destroy(gameObject);
	}
}
