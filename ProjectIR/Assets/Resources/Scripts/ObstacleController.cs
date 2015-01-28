using UnityEngine;
using System.Collections;



public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleTemplate;
	enum spawnPatterns {nullPattern, Basic, Advanced, Painful};
	public float patternTimer;
	spawnPatterns previousPattern, currentPattern;
	int numObstacles;
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
			currentPattern = newPattern(previousPattern);
			Debug.Log(currentPattern);
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

	void setPatternTimer(spawnPatterns currentPattern){
		switch (currentPattern) {
		case spawnPatterns.nullPattern:
			patternTimer = 1;
			break;
		case spawnPatterns.Basic:
		case spawnPatterns.Advanced:
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
		if(previousPattern != spawnPatterns.nullPattern){
			return spawnPatterns.nullPattern;
		}
		spawnPatterns nextPattern = (spawnPatterns)(3 * Random.value);
		switch(nextPattern){
		case spawnPatterns.nullPattern:
			if(nextPattern == previousPattern){
				numObstacles = 15;
				return spawnPatterns.Basic;
			}
			return spawnPatterns.nullPattern;

		case spawnPatterns.Basic:
			if(nextPattern == previousPattern){
				numObstacles = 16;
				return spawnPatterns.Advanced;
			}
			numObstacles = 15;
			return spawnPatterns.Basic;

		case spawnPatterns.Advanced:
			if(nextPattern == previousPattern){
				numObstacles = 15;
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

	void executePattern(spawnPatterns pattern){
		switch (pattern) {
		case spawnPatterns.nullPattern:
			break;
		case spawnPatterns.Basic:
			/*if(timeTest(2.9f, 16)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else*/ if(timeTest(2.8f, 15)){
				spawnObstacle(1.0f);
				numObstacles--;
			} else if(timeTest(2.6f, 14)){
				spawnObstacle(2.0f);
				numObstacles--;
			} else if(timeTest(2.4f, 13)){
				spawnObstacle(3.0f);
				numObstacles--;
			} else if(timeTest(2.2f, 12)){
				spawnObstacle(-0.5f);
				numObstacles--;
			} else if(timeTest(2.0f, 11)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if(timeTest(1.8f, 10)){
				spawnObstacle(-2.5f);
				numObstacles--;
			} else if(timeTest(1.6f, 9)){
				spawnObstacle(-3.0f);
				numObstacles--;
			} else if(timeTest(1.3f, 8)){
				spawnObstacle(-0.5f);
				numObstacles--;
			} else if(timeTest(1.1f, 7)){
				spawnObstacle(0.5f);
				numObstacles--;
			} else if(timeTest(0.9f, 6)){
				spawnObstacle(1.5f);
				numObstacles--;
			} else if(timeTest(0.7f, 5)){
				spawnObstacle(1.5f);
				numObstacles--;
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if(timeTest(0.5f, 3)){
				spawnObstacle(1.5f);
				numObstacles--;
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if(timeTest(0.3f, 1)){
				spawnObstacle(-1.5f);
				numObstacles--;
			}
			break;
		case spawnPatterns.Advanced:
			if(timeTest(2.9f, 16)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if(timeTest(2.7f, 15)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if(timeTest(2.5f, 14)){
				spawnObstacle(-1.5f);
				numObstacles--;
			} else if(timeTest(2.3f, 13)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if(timeTest(2.1f, 12)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if(timeTest(1.9f, 11)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if(timeTest(1.7f, 10)){
				spawnObstacle(1.0f);
				numObstacles--;
			} else if(timeTest(1.5f, 9)){
				spawnObstacle(-2.0f);
				numObstacles--;
			} else if(timeTest(1.3f, 8)){
				spawnObstacle(0.5f);
				numObstacles--;
			} else if(timeTest(1.1f, 7)){
				spawnObstacle(-0.5f);
				numObstacles--;
			} else if(timeTest(0.9f, 6)){
				spawnObstacle(-2.5f);
				numObstacles--;
			} else if(timeTest(0.7f, 5)){
				spawnObstacle(2.5f);
				numObstacles--;
			} else if(timeTest(0.5f, 4)){
				spawnObstacle(0.0f);
				numObstacles--;
			} else if(timeTest(0.3f, 3)){
				spawnObstacle(-1.0f);
				numObstacles--;
			} else if(timeTest(0.1f, 2)){
				spawnObstacle(-2.0f);
				numObstacles--;
			} else if(timeTest(0.0f, 1)){
				spawnObstacle(2.0f);
				numObstacles--;
			}
			break;

		case spawnPatterns.Painful:

			break;
		}
	}


	bool timeTest(float time, int ObstacleNumber){
		if((patternTimer > time) && (patternTimer < time + 0.1f) && (numObstacles == ObstacleNumber)){
			return true;
		}
		return false;
	}

}
















