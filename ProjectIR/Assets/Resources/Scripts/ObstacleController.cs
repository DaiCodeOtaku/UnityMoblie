using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public static GameObject ObstacleTemplate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnObstacle()){
			GameObject Obstacle = new GameObject();
			Obstacle = ObstacleTemplate;
			Instantiate(Obstacle);
		}
	}

	bool spawnObstacle(){
		if ((Random.value * 1000.0f) <= 10) {
			return true;
		}
		return false;
	}
}
