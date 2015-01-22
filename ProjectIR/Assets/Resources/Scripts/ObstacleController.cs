using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleTemplate;
	enum spawnPatterns {nullPattern, Basic, Advanced};
	public float patternTimer;
	int previousPattern, currentPattern, numObstacles;
	float spawnHeight, spawnDepth;

	// Use this for initialization
	void Start () {
		patternTimer = 2;
		previousPattern = 0;
		spawnHeight = 7.0f;
		spawnDepth = -0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		patternTimer -= Time.deltaTime;

		if (patternTimer < 0) {
			currentPattern = (int)newPattern((spawnPatterns)previousPattern);
			setPatternTimer(currentPattern);
		}

		executePattern(currentPattern);		

		/*if(spawnObstacle()){
			//Random.Range(-3.1f, 3.1f);
			//ObstacleTemplate.transform.Translate(new Vector3(10, 0, 0));
			ObstacleTemplate.transform.Translate(new Vector3(Random.Range(-3.1f, 3.1f), 7, -0.5f));
			GameObject.Instantiate((GameObject)ObstacleTemplate);
			resetObstacle();
		}*/

	}

	void setPatternTimer(int currentPattern){
		switch (currentPattern) {
		case 0:
			patternTimer = 1;
			break;
		case 1:
		case 2:
		default:
			patternTimer = 3;
			break;
		}
	}

	bool spawnObstacle(){
		if ((Random.value * 1000.0f) <= 100) {
			return true;
		}
		return false;
	}

	spawnPatterns newPattern(spawnPatterns previousPattern){
		int nextPattern = (int)(3 * Random.value);
		switch(nextPattern){
		case 0:
			if(nextPattern == (int)previousPattern){
				numObstacles = 16;
				return spawnPatterns.Basic;
			}
			return spawnPatterns.nullPattern;

		case 1:
			if(nextPattern == (int)previousPattern){
				numObstacles = 16;
				return spawnPatterns.Advanced;
			}
			numObstacles = 16;
			return spawnPatterns.Basic;

		case 2:
			if(nextPattern == (int)previousPattern){
				numObstacles = 16;
				return spawnPatterns.Basic;
			}
			numObstacles = 16;
			return spawnPatterns.Advanced;
		}
		return 0;
	}

	void resetObstacle(){
		ObstacleTemplate.transform.Translate(-ObstacleTemplate.transform.position);
	}

	void spawnObstacle(float x){
		ObstacleTemplate.transform.Translate(new Vector3(x, spawnHeight, spawnDepth));
		GameObject.Instantiate((GameObject)ObstacleTemplate);
		resetObstacle();
	}

	void executePattern(int pattern){
		switch (pattern) {
		case 0:
			break;
		case 1:
			if((patternTimer > 2.9) && (patternTimer < 3) && (numObstacles == 16)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if((patternTimer > 2.8) && (patternTimer < 2.9) && (numObstacles == 15)){
				spawnObstacle(1.0f);
				numObstacles--;
			} else if((patternTimer > 2.6) && (patternTimer	< 2.7) && (numObstacles == 14)){
				spawnObstacle(2.0f);
				numObstacles--;
			} else if((patternTimer > 2.4) && (patternTimer < 2.5) && (numObstacles == 13)){
				spawnObstacle(3.0f);
				numObstacles--;
			} else if((patternTimer > 2.2) && (patternTimer < 2.3) && (numObstacles == 12)){
				spawnObstacle(-0.5f);
				numObstacles--;
			} else if((patternTimer > 2) && (patternTimer < 2.1) && (numObstacles == 11)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if((patternTimer > 1.8) && (patternTimer < 1.9) && (numObstacles == 10)){
				spawnObstacle(-2.5f);
				numObstacles--;
			} else if((patternTimer > 1.6) && (patternTimer < 1.7) && (numObstacles == 9)){
				spawnObstacle(-3.0f);
				numObstacles--;
			} else if((patternTimer > 1.3) && (patternTimer < 1.4) && (numObstacles == 8)){
				spawnObstacle(-0.5f);
				numObstacles--;
			} else if((patternTimer > 1.1) && (patternTimer < 1.2) && (numObstacles == 7)){
				spawnObstacle(0.5f);
				numObstacles--;
			} else if((patternTimer > 0.9) && (patternTimer < 1.0) && (numObstacles == 6)){
				spawnObstacle(1.5f);
				numObstacles--;
			} else if((patternTimer > 0.7) && (patternTimer < 0.8) && (numObstacles == 5)){
				spawnObstacle(1.5f);
				numObstacles--;
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if((patternTimer > 0.5) && (patternTimer < 0.6) && (numObstacles == 3)){
				spawnObstacle(1.5f);
				numObstacles--;
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if((patternTimer > 0.3) && (patternTimer < 0.4) && (numObstacles == 1)){
				spawnObstacle(-1.5f);
				numObstacles--;
			}
			break;
		case 2:
			if((patternTimer > 2.9) && (patternTimer < 3.0) && (numObstacles == 16)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if((patternTimer > 2.7) && (patternTimer < 2.8) && (numObstacles == 15)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if((patternTimer > 2.5) && (patternTimer < 2.6) && (numObstacles == 14)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if((patternTimer > 2.3) && (patternTimer < 2.4) && (numObstacles == 13)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if((patternTimer > 2.1) && (patternTimer < 2.2) && (numObstacles == 12)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if((patternTimer > 1.9) && (patternTimer < 2.0) && (numObstacles == 11)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if((patternTimer > 1.7) && (patternTimer < 1.8) && (numObstacles == 10)){
				spawnObstacle(1.0f);
				numObstacles--;
			} else if((patternTimer > 1.5) && (patternTimer < 1.6) && (numObstacles == 9)){
				spawnObstacle(-2.0f);
				numObstacles--;
			} else if((patternTimer > 1.3) && (patternTimer < 1.4) && (numObstacles == 8)){
				spawnObstacle(0.5f);
				numObstacles--;
			} else if((patternTimer > 1.1) && (patternTimer < 1.2) && (numObstacles == 7)){
				spawnObstacle(-0.5f);
				numObstacles--;
			} else if((patternTimer > 0.9) && (patternTimer < 1.0) && (numObstacles == 6)){
				spawnObstacle(-2.5f);
				numObstacles--;
			} else if((patternTimer > 0.7) && (patternTimer < 0.8) && (numObstacles == 5)){
				spawnObstacle(2.5f);
				numObstacles--;
			} else if((patternTimer > 0.5) && (patternTimer < 0.6) && (numObstacles == 4)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if((patternTimer > 0.3) && (patternTimer < 0.4) && (numObstacles == 3)){
				spawnObstacle(-1.0f);
				numObstacles--;
			} else if((patternTimer > 0.1) && (patternTimer < 0.2) && (numObstacles == 2)){
				spawnObstacle(-2.0f);
				numObstacles--;
			} else if((patternTimer > 0.0) && (patternTimer < 0.1) && (numObstacles == 1)){
				spawnObstacle(2.0f);
				numObstacles--;
			}
			break;
		}
	}

}
















