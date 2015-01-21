using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleTemplate;
	enum spawnPatterns {Basic, Advanced};
	float patternTimer;

	// Use this for initialization
	void Start () {
		patternTimer = 5;
	}
	
	// Update is called once per frame
	void Update () {
		patternTimer -= Time.deltaTime;
		if(patternTimer < 0){
			newPattern((spawnPatterns)1);
		}
		if(spawnObstacle()){
			//Random.Range(-3.1f, 3.1f);
			//ObstacleTemplate.transform.Translate(new Vector3(10, 0, 0));
			ObstacleTemplate.transform.Translate(new Vector3(Random.Range(-3.1f, 3.1f), 7, -0.5f));
			GameObject.Instantiate((GameObject)ObstacleTemplate);
			resetObstacle();
		}
	}

	bool spawnObstacle(){
		if ((Random.value * 1000.0f) <= 100) {
			return true;
		}
		return false;
	}

	spawnPatterns newPattern(spawnPatterns previousPattern){
		int nextPattern = (int)((2 * Random.value)+1);
		switch(nextPattern){
		case 0:
			if(nextPattern == (int)previousPattern){
				return (spawnPatterns)1;
			}
			return 0;

		case 1:
			if(nextPattern == (int)previousPattern){
				return 0;
			}
			return (spawnPatterns)1;

		}
		return 0;
	}

	void resetObstacle(){
		ObstacleTemplate.transform.Translate(-ObstacleTemplate.transform.position);
	}



}
