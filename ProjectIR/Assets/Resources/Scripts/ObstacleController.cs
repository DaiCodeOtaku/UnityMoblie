using UnityEngine;
using System.Collections;



public class ObstacleController : MonoBehaviour {

	public GameObject ObstacleTemplate;
	enum spawnPatterns {nullPattern, Basic, Advanced, Painful};
	public float patternTimer;
	spawnPatterns previousPattern, currentPattern;
	int patternOffset;
	float spawnHeight, spawnDepth;

	int numBasic = 41;
	float[] basicPattern = {	0.2f, 1, 1.0f,			0.2f, 1, 2.0f,			0.2f, 1, 3.0f, 				
								0.2f, 1, -0.5f,			0.2f, 1, -1.5f,			0.2f, 1, -2.5f, 			
								0.2f, 1, -3.0f,			0.3f, 1, -0.5f,			0.2f, 1, 0.5f,				
								0.2f, 1, 1.5f,			0.2f, 2, 1.5f, -1.5f,	0.2f, 2, 1.5f, -1.5f,	
								0.2f, 1, -1.5f,			0.3f};

	int numAdvanced = 49;
	float[] advancedPattern = {	0.1f, 1, -1.5f,			0.2f, 1, -1.5f,			0.2f, 1, -1.5f,			//9
								0.2f, 1, 0.0f,			0.2f, 1, 0.0f,			0.2f, 1, 0.0f,			//9
								0.2f, 1, 1.0f,			0.2f, 1, -2.0f,			0.2f, 1, 0.5f,			//9
								0.2f, 1, -0.5f,			0.2f, 1, -2.5f,			0.2f, 1, 2.5f,			//10
								0.2f, 1, 0.0f,			0.2f, 1, -1.0f, 		0.2f, 1, -2.0f,			//9
								0.1f, 1, 2.0f,			0.0f};											//4

	int numPainful = 75;
	float[] painfulPattern = {	0.1f, 2, 0.0f, 2.0f,	0.1f, 1, -0.5f, 		0.1f, 1, 0.5f,			//10
								0.1f, 1, -1.0f, 		0.1f, 1, 1.0f,			0.1f, 1, -1.5f,			//9
								0.1f, 1, 1.5f,			0.2f, 2, 2.0f, 3.0f,	0.1f, 1, 2.5f,			//10
								0.1f, 1, -3.0f,			0.2f, 2, -2.5f, 1.5f,	0.2f, 2, -2.0f, 1.0f,	//11
								0.2f, 2, -1.5f, 0.5f, 	0.1f, 1, 3.0f,			0.1f, 1, 2.5f,			//10
								0.1f, 1, 2.0f, 			0.1f, 1, 1.5f, 			0.1f, 1, 1.0f,			//9
								0.1f, 1, -1.0f, 		0.1f, 1, -1.5f, 		0.1f, 1, -2.0f,			//9
								0.1f, 1, -2.5f, 		0.1f, 1, 3.0f, 			0.0f};					//7


	// Use this for initialization
	void Start () {
		patternTimer = 2;
		previousPattern = 0;
		patternOffset = 0;
		spawnHeight = 7.0f;
		spawnDepth = -0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		patternTimer -= Time.deltaTime;

		executePattern(currentPattern);

		if (patternTimer < 0) {
			currentPattern = newPattern();
			Debug.Log(currentPattern);
			setPatternTimer(currentPattern);
		}
	}

	void setPatternTimer(spawnPatterns currentPattern){
		switch (currentPattern) {
		case spawnPatterns.nullPattern:
			patternTimer = 1;
			break;
		case spawnPatterns.Basic:
			patternTimer = basicPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.Advanced:
			patternTimer = advancedPattern[0];
			patternOffset = 1;
			break;
		case spawnPatterns.Painful:
			patternTimer = painfulPattern[0];
			patternOffset = 1;
			break;
		}
	}

	bool spawnObstacle(){
		if ((Random.value * 1000.0f) <= 100) {
			return true;
		}
		return false;
	}

	spawnPatterns newPattern(){
		spawnPatterns nextPattern = (spawnPatterns)(4 * Random.value);
		switch(nextPattern){
		case spawnPatterns.nullPattern:
			if(nextPattern == previousPattern){
				return spawnPatterns.Basic;
			}
			previousPattern = spawnPatterns.nullPattern;
			return spawnPatterns.nullPattern;

		case spawnPatterns.Basic:
			if(nextPattern == previousPattern){
				return spawnPatterns.Advanced;
			}
			previousPattern = spawnPatterns.Basic;
			return spawnPatterns.Basic;

		case spawnPatterns.Advanced:
			if(nextPattern == previousPattern){
				return spawnPatterns.Basic;
			}
			previousPattern = spawnPatterns.Advanced;
			return spawnPatterns.Advanced;

		case spawnPatterns.Painful:
			if(nextPattern == previousPattern){
				return spawnPatterns.Advanced;
			}
			previousPattern = spawnPatterns.Painful;
			return spawnPatterns.Painful;
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
			if(patternOffset < numBasic){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < basicPattern[patternOffset]; i++){
						spawnObstacle(basicPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = basicPattern[patternOffset];
					patternOffset++;
				}
			}
			break;
		case spawnPatterns.Advanced:
			if(patternOffset < numAdvanced){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < advancedPattern[patternOffset]; i++){
						spawnObstacle(advancedPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = advancedPattern[patternOffset];
					patternOffset++;
				}
			}
			break;

		case spawnPatterns.Painful:
			if(patternOffset < numPainful){
				if(patternTimer < 0){
					int i = 0;
					for(i = 0; i < painfulPattern[patternOffset]; i++){
						spawnObstacle(painfulPattern[patternOffset + i + 1]);
					}
					patternOffset += i + 1;
					patternTimer = painfulPattern[patternOffset];
					patternOffset++;
				}
			}
			break;
		}
	}

}
















